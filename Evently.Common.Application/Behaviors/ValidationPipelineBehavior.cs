using Dapper;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Reflection;

namespace Evently.Common.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators) :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
    {
        public async Task<TResponse> Handle(TRequest request
            , RequestHandlerDelegate<TResponse> next
            , CancellationToken cancellationToken)
        {

            ValidationFailure[] validationFailures = await ValidateAsync(request);

            if (validationFailures.Length == 0)
            {
                return await next();
            }

            if (typeof(TResponse).IsGenericType
                && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                Type resType = typeof(TResponse).GetGenericArguments()[0];

                MethodInfo? method = typeof(Result<>)
                    .MakeGenericType(resType)
                    .GetMethod(nameof(Result<object>.ValidationFailure));

                if (method is not null)
                {
                    return (TResponse)method.Invoke(null, [CreateValidationError(validationFailures)]);
                }
            }
            else if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
            }

            throw new ValidationException(validationFailures);
        }

        private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
            new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());

        private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
        {
            if (!validators.Any())
            {
                return [];
            }
            var context = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResult = await Task.WhenAll(
                validators.Select(x => x.ValidateAsync(context)));

            ValidationFailure[] validationFailures = validationResult.
                Where(x => x.IsValid == false)
                .SelectMany(x => x.Errors)
                .ToArray();

            return validationFailures;
        }
    }
}

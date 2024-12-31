using FluentValidation;

namespace Evently.Modules.Users.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Register.FirstName).NotNull().WithMessage(UserValidatorErrors.UserFirstNameNotNull);
            RuleFor(x => x.Register.LastName).NotNull().WithMessage(UserValidatorErrors.UserLastNameNotNull);
            RuleFor(x => x.Register.Email).NotNull().WithMessage(UserValidatorErrors.UserEmailNotNull);
            RuleFor(x => x.Register.Email).EmailAddress().WithMessage(UserValidatorErrors.UserEmailInvalid);
        }
    }
}

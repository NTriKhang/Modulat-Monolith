using Evently.Modules.Events.Domain.Events;
using FluentValidation;



namespace Evently.Modules.Events.Application.Events.CreateEvent
{
    internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.eventCreateDto.Title).NotEmpty().WithMessage(EventValidatorErrors.TitleNotNull);
            RuleFor(x => x.eventCreateDto.Description).NotEmpty().WithMessage(EventValidatorErrors.DescriptionNotNulL);
            RuleFor(x => x.eventCreateDto.Location).NotEmpty().WithMessage(EventValidatorErrors.LocationNotNull);
            RuleFor(x => x.eventCreateDto.StartsAtUtc).NotEmpty().WithMessage(EventValidatorErrors.StartsAtNotNull);
            RuleFor(x => x.eventCreateDto.EndsAtUtc).Must((cmd, endsAtUtc) => endsAtUtc > cmd.eventCreateDto.StartsAtUtc)
                .When(x => x.eventCreateDto.EndsAtUtc.HasValue)
                .WithMessage(EventValidatorErrors.EndDateProceedStartDate);
        }
    }
}


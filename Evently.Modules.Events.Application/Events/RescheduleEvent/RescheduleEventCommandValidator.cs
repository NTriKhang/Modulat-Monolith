using FluentValidation;

namespace Evently.Modules.Events.Application.Events.RescheduleEvent
{
    internal sealed class RescheduleEventCommandValidator : AbstractValidator<RescheduleEventCommand>
    {
        public RescheduleEventCommandValidator()
        {
            RuleFor(x => x.reschedule.EventId).NotEmpty().WithMessage(EventValidatorErrors.EventIdNotNull);
            RuleFor(x => x.reschedule.StartsAtUtc).NotEmpty().WithMessage(EventValidatorErrors.StartsAtNotNull);
            RuleFor(x => x.reschedule.EndsAtUtc).GreaterThanOrEqualTo(x => x.reschedule.StartsAtUtc).WithMessage(EventValidatorErrors.EndDateProceedStartDate);
        }
    }
}

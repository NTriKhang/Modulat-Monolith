using FluentValidation;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice
{
    internal class UpdateTicketTypePriceCommandValidator : AbstractValidator<UpdateTicketTypePriceCommand>
    {
        public UpdateTicketTypePriceCommandValidator()
        {
            RuleFor(x => x.TicketTypeId).NotEmpty().WithMessage(TicketTypeValidatorErrors.TicketTypeIdNotNull);
            RuleFor(x => x.newPrice).GreaterThanOrEqualTo(0).WithMessage(TicketTypeValidatorErrors.PriceCannotBeNegative);
        }
    }
}

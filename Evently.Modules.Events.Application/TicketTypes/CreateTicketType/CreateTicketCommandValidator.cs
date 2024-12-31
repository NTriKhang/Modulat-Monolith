using FluentValidation;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType
{
    internal class CreateTicketCommandValidator : AbstractValidator<CreateTicketTypeCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(x => x.request.EventId).NotEmpty().WithMessage(TicketTypeValidatorErrors.EventIdNotNull);
            RuleFor(x => x.request.Name).NotEmpty().WithMessage(TicketTypeValidatorErrors.NameNotNull);
            RuleFor(x => x.request.Currency).NotEmpty().WithMessage(TicketTypeValidatorErrors.CurrencyNotNull);

            RuleFor(x => x.request.Price).GreaterThanOrEqualTo(0).WithMessage(TicketTypeValidatorErrors.PriceCannotBeNegative);
            RuleFor(x => x.request.Quantity).GreaterThanOrEqualTo(0).WithMessage(TicketTypeValidatorErrors.QuantityCannotBeNegative);
        }
    }
}

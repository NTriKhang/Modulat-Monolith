using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.TicketTypes
{
    internal class TicketTypePriceChangedDomainEvent(Guid TicketTypeId, decimal Price) : DomainEvent
    {
        public Guid TicketTypeId { get; init; } = TicketTypeId;
        public decimal Price { get; init; } = Price;
    }
}

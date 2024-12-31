using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.TicketTypes
{
    internal class TicketTypeCreateDomainEvent(Guid TicketTypeId) : DomainEvent
    {
        public Guid TicketTypeId { get; init; } = TicketTypeId;
    }
}

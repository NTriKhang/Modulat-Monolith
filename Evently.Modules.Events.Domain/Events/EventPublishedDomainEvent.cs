using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events
{

    internal sealed class EventPublishedDomainEvent(Guid Id) : DomainEvent
    {
        public Guid EventId { get; init; } = Id;
    }
}

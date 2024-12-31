using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events
{
    internal sealed class EventCanceledDomainEvent(Guid Id) : DomainEvent
    {
        public Guid Id { get; init; } = Id;
    }
}

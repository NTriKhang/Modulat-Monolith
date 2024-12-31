using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events
{
    public sealed class EventRescheduledDomainEvent(Guid Id, DateTime StartsAtUtc, DateTime? EndsAtUtc) : DomainEvent
    {
        public Guid Id { get; init; } = Id;
        public DateTime StartsAtUtc { get; init; } = StartsAtUtc;
        public DateTime? EndsAtUtc { get; init; } = EndsAtUtc; 
    }
}

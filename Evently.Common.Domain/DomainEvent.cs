namespace Evently.Common.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            Id = new Guid();
            OccurredDate = DateTime.Now;
        }
        protected DomainEvent(Guid Id, DateTime OccuredDate)
        {
            this.Id = Id;
            OccurredDate = OccuredDate;
        }
        public Guid Id { get; init; }
        public DateTime OccurredDate { get; init; }
    }
}

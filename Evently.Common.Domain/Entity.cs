namespace Evently.Common.Domain
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _events = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.ToList();
        protected Entity()
        {
        }
        public void ClearDomainEvent()
        {
            _events.Clear();
        }
        protected void Raise(IDomainEvent e)
        {
            _events.Add(e);
        }
    }
}

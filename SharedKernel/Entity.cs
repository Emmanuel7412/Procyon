namespace SharedKernel
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected Entity(Guid id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public Guid Id { get; init; }

        public List<IDomainEvent> DomainEvents => [.. _domainEvents];

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}

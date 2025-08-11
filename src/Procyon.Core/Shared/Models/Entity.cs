using Core.Shared;

namespace Procyon.Core.Shared.Models
{
    public abstract class Entity<T> : IEntity<T>
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected Entity(T id)
        {
            Id = id;
        }

        protected Entity()
        {
            Id = default!;
        }

        public T Id { get; set; }

        public List<IDomainEvent> DomainEvents => [.. _domainEvents];

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}

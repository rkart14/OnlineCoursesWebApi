using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourses.Domain.SeedWork
{
    public class EntityBase<TEntity, TIdentity>
    {

        public virtual TIdentity Id { get; protected set; }
        public virtual EntityStatus Status { get; protected set; } = EntityStatus.Active;
        public virtual Guid ArchiveIdentifier { get; protected set; }

        public int Version { get; protected set; }

        private List<Event> _domainEvents;
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

        protected EntityBase()
        {
            Version = 0;
            ArchiveIdentifier = new Guid();
            _domainEvents = new List<Event>();
        }

        public void AddDomainEvent(Event eventItem)
        {
            _domainEvents ??= new List<Event>();
            Version++;
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(Event eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected void Publish(Event e)
        {
            AddDomainEvent(e);
        }

        public bool HasEvent<T>()
        {
            return _domainEvents.Any(x => typeof(T) == x.GetType());
        }
    }
}

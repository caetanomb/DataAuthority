using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAuthority.Domain
{
    public class Entity
    {
        int _Id;
        public virtual int Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        private List<INotification> _domainEvents;
        public List<INotification> DomainEvents => _domainEvents;

        public Entity(int id)
        {
            _Id = id;
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }        
    }
}

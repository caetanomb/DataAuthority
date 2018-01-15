using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAuthority.Domain.Event
{
    public class PayLoadCreatedDomainEvent : INotification
    {
        public int ProvidedPayLoadId { get; private set; }
        public string Origin { get; private set; }

        public PayLoadCreatedDomainEvent(int providedPayLoadId, string origin)
        {
            ProvidedPayLoadId = providedPayLoadId;
            Origin = origin;
        }
    }
}

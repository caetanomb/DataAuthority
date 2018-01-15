using DataAuthority.Domain.Event;
using DataAuthority.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAuthority.Domain
{
    public class PayLoad : Entity
    {
        public int ProvidedPayLoadId { get; private set; }
        public string Origin { get; private set; }
        public string Data { get; private set; }

        public PayLoad(int id, int providedPayLoadId, string origin, string data)
            : base(id)
        {            
            ProvidedPayLoadId = providedPayLoadId;
            Origin = origin;
            Data = data;

            ConstructorActions();

        }

        public PayLoad(int providedPayLoadId, string origin, string data)
            : base(0)
        {
            ProvidedPayLoadId = providedPayLoadId;
            Origin = origin;
            Data = data;

            ConstructorActions();
        }

        private void ConstructorActions()
        {
            //try
            //{
            //    Convert.FromBase64String(Data);
            //}
            //catch
            //{
            //    throw new DataAuthorityDomainException("Invalid Base64 data");
            //}

            if (Id == 0)
                AddDomainEvent(new PayLoadCreatedDomainEvent(ProvidedPayLoadId, Origin));
        }
    }
}

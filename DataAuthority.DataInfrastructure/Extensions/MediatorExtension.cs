using DataAuthority.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAuthority.DataInfrastructure.Extensions
{
    public static class MediatorExtension
    {
        public static void DispatchDomainEvents(this IMediator mediator, Entity entity)
        {
            var domainEvents = entity.DomainEvents
                .ToList();

            entity.DomainEvents.Clear();

            domainEvents
                .ForEach((domainEvent) => {
                    mediator.Publish(domainEvent);
                });
        }
    }
}

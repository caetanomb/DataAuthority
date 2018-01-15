using DataAuthority.Domain;
using DataAuthority.Domain.Event;
using DataAuthority.Domain.Repository;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAuthority.Base64Left.API.DomainEventHandlers
{
    public class PayLoadCreatedDomainEventHandler : INotificationHandler<PayLoadCreatedDomainEvent>
    {
        private readonly IDataAuthorityRepository _dataAuthorityRepository;

        public PayLoadCreatedDomainEventHandler(IDataAuthorityRepository dataAuthorityRepository)
        {
            _dataAuthorityRepository = dataAuthorityRepository ?? throw new ArgumentNullException("DataAuthorityRepository null");
        }

        public async Task Handle(PayLoadCreatedDomainEvent payLoadCreatedEvent, CancellationToken cancellationToken)
        {
            string oppositeOrigin = (payLoadCreatedEvent.Origin == "Left" ? "Right" : "Left");
            
            List<PayLoad> payLoadList = 
                await _dataAuthorityRepository.GetPaylodAsync(payLoadCreatedEvent.ProvidedPayLoadId);

            if (payLoadList.Where(a => a.Origin == oppositeOrigin).Any())
            {
                PayLoad left = payLoadList.FirstOrDefault(a => a.Origin == "Left");
                PayLoad right = payLoadList.FirstOrDefault(a => a.Origin == "Right");

                DataValidator validator = new DataValidator(left.Data, right.Data);
                DataDiffInsight dataDiff = validator.Diff();

                string diffResult = JsonConvert.SerializeObject(dataDiff);

                await _dataAuthorityRepository.AddResult(payLoadCreatedEvent.ProvidedPayLoadId,
                    diffResult);
            }
        }
    }
}

using DataAuthority.ApplicationService.Commands;
using DataAuthority.Domain;
using DataAuthority.Domain.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAuthority.ApplicationService.CommandHandlers
{
    /// <summary>
    /// Command triggered by API - Post Method - It stores the request content
    /// </summary>
    public class CreatePayLoadCommandHandler : IRequestHandler<CreatePayLoadCommand, bool>
    {
        private readonly IDataAuthorityRepository _dataAuthorityRepository;

        public CreatePayLoadCommandHandler(IDataAuthorityRepository dataAuthorityRepository)
        {
            _dataAuthorityRepository = dataAuthorityRepository;
        }

        public Task<bool> Handle(CreatePayLoadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                PayLoad payLoad = new PayLoad(request.ProvidedPayLoadId, request.Origin, request.Data);

                PayLoad newId = _dataAuthorityRepository.AddPayLoad(payLoad);

                return Task.FromResult(newId.Id > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}

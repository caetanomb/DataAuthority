using DataAuthority.DataInfrastructure.DataBaseInterface;
using DataAuthority.Domain;
using DataAuthority.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DataAuthority.DataInfrastructure.Extensions;

namespace DataAuthority.DataInfrastructure.Repositories
{
    public class DataAuthorityRepository : IDataAuthorityRepository
    {
        private IMediator _mediator;
        private IDataBase _dataBase;

        public DataAuthorityRepository(IDataBase dataBase, IMediator mediator)
        {
            _dataBase = dataBase;
            _mediator = mediator;
        }
        public PayLoad AddPayLoad(PayLoad payLoad)
        {
            int id = 0;

            DataModels.PayLoadDataModel payLoadEntity =
                _dataBase.GetPaylod(payLoad.ProvidedPayLoadId, payLoad.Origin);

            if (payLoadEntity != null)
            {
                id = payLoadEntity.Id;
                payLoadEntity.Data = payLoad.Data;
                _dataBase.UpdatePayLoad(payLoadEntity);
            }
            else
            {
                id = _dataBase.AddPayLoad(new DataModels.PayLoadDataModel()
                {
                    ProvidedPayLoadId = payLoad.ProvidedPayLoadId,
                    Origin = payLoad.Origin,
                    Data = payLoad.Data
                });
            }

            _mediator.DispatchDomainEvents(payLoad);

            DataModels.PayLoadDataModel newlyEntity = _dataBase.GetPaylodById(id);

            if (newlyEntity == null)
                return null;
            else
                return new PayLoad(newlyEntity.Id, newlyEntity.ProvidedPayLoadId, newlyEntity.Origin, newlyEntity.Data);
        }

        public async Task AddResult(int providedPayLoadId, string diffResult)
        {            
            DataModels.PayLoadDataModel existingDiffResult =
                await _dataBase.GetResult(providedPayLoadId, "DiffResult");

            if (existingDiffResult != null)
            {                
                existingDiffResult.Data = diffResult;
                await _dataBase.UpdateDiffResult(existingDiffResult);
            }
            else
            {
                await _dataBase.AddResult(providedPayLoadId, "DiffResult", diffResult);
            }            
        }

        public PayLoad GetPayLoadById(int id)
        {
            DataModels.PayLoadDataModel result = _dataBase.GetPaylodById(id);

            return new PayLoad(result.Id, result.ProvidedPayLoadId, result.Origin, result.Data);
        }        

        public async Task<List<PayLoad>> GetPaylodAsync(int providedPayLoadId)
        {
            List<DataModels.PayLoadDataModel> list = 
                await _dataBase.GetPaylodAsync(providedPayLoadId);

            return list.Select(a => new PayLoad(a.Id, a.ProvidedPayLoadId, a.Origin, a.Data)).ToList();
        }
    }
}

using DataAuthority.DataInfrastructure.DataModels;
using DataAuthority.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAuthority.DataInfrastructure.DataBaseInterface
{
    public interface IDataBase
    {
        int AddPayLoad(PayLoadDataModel payLoad);
        Task<List<PayLoadDataModel>> GetPaylodAsync(int providedPayLoadId);
        List<PayLoadDataModel> GetPaylod(int providedPayLoadId);
        PayLoadDataModel GetPaylodById(int id);        
        Task AddResult(int providedPayLoadId, string origin, string diffResult);
        Task<PayLoadDataModel> GetResult(int providedPayLoadId, string origin);
        PayLoadDataModel GetPaylod(int providedPayLoadId, string origin);
        void UpdatePayLoad(PayLoadDataModel payLoadDataModel);
        Task UpdateDiffResult(PayLoadDataModel payLoadDataModel);
    }
}

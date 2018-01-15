using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAuthority.Domain.Repository
{
    public interface IDataAuthorityRepository
    {
        PayLoad AddPayLoad(PayLoad payLoad);
        PayLoad GetPayLoadById(int id);
        Task<List<PayLoad>> GetPaylodAsync(int ProvidedPayLoadId);
        Task AddResult(int providedPayLoadId, string diffResult);
    }
}

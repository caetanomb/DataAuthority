using DataAuthority.DataInfrastructure.DataBaseInterface;
using DataAuthority.DataInfrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataAuthority.Domain;

namespace DataAuthority.SqlServerEF
{
    public class SqlServerDataBase : IDataBase
    {
        private readonly DataAuthorityContext _context;

        public SqlServerDataBase(DataAuthorityContext context)
        {
            _context = context ?? throw new ArgumentException("DataAuthorityContext");
        }

        public int AddPayLoad(PayLoadDataModel payLoad)
        {
            var payLoadEntity = _context.PayLoads.Where(a => a.ProvidedPayLoadId == payLoad.ProvidedPayLoadId &&
                                        a.Origin == payLoad.Origin).FirstOrDefault();

            if (payLoadEntity != null)
            {

            }
            else
            {
                _context.PayLoads.Add(payLoad);
                _context.SaveChanges(true);
            }

            return payLoad.Id;
        }

        public Task AddResult(int providedPayLoadId, string origin, string diffResult)
        {
            return Task.FromResult(
                _context.PayLoads.Add(new PayLoadDataModel()
                {
                    ProvidedPayLoadId = providedPayLoadId,
                    Origin = origin,
                    Data = diffResult
                })                
            ).ContinueWith((obj) => {                
                _context.SaveChanges(true);
            });
        }

        public List<PayLoadDataModel> GetPaylod(int providedPayLoadId)
        {
            return _context.PayLoads.Where(a => a.ProvidedPayLoadId == providedPayLoadId).ToList();
        }

        public PayLoadDataModel GetPaylod(int providedPayLoadId, string origin)
        {
            return _context.PayLoads.Where(a => a.ProvidedPayLoadId == providedPayLoadId &&
                                        a.Origin == origin).FirstOrDefault();

        }

        public Task<List<PayLoadDataModel>> GetPaylodAsync(int providedPayLoadId)
        {
            return Task.FromResult(
                 _context.PayLoads
                         .Where(a => a.ProvidedPayLoadId == providedPayLoadId).ToList()
            );
        }

        public PayLoadDataModel GetPaylodById(int id)
        {
            return _context.PayLoads.Find(id);
        }

        public Task<PayLoadDataModel> GetResult(int providedPayLoadId, string origin)
        {
            return Task.FromResult(
                _context.PayLoads.Where(a => a.ProvidedPayLoadId == providedPayLoadId &&
                                             a.Origin == origin).FirstOrDefault());
        }

        public async Task UpdateDiffResult(PayLoadDataModel payLoadDataModel)
        {
            _context.Entry(payLoadDataModel).State = EntityState.Modified;
            await _context.SaveChangesAsync(true);
        }

        public void UpdatePayLoad(PayLoadDataModel payLoadDataModel)
        {            
            _context.Entry(payLoadDataModel).State = EntityState.Modified;
            _context.SaveChanges(true);
        }
    }
}

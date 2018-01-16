using DataAuthority.DataInfrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAuthority.SqlServerEF.Seed
{
    public static class Seeding
    {
        public static void EnsureSeeded(this DataAuthorityContext context)
        {
            if (!context.PayLoads.Any())
            {
                List<PayLoadDataModel> list = new List<PayLoadDataModel>()
                {

                    new PayLoadDataModel()
                    {
                        ProvidedPayLoadId = 10,
                        Origin = "Left",
                        Data = "{\"id\":1,\"name\":\"Test\",\"address\":\"Avenue 1\"}"
                    },
                    new PayLoadDataModel()
                    {
                        ProvidedPayLoadId = 10,
                        Origin = "Right",
                        Data = "{\"id\":1,\"name\":\"Test\",\"address\":\"Avenue 1\"}"
                    },
                    new PayLoadDataModel()
                    {
                        ProvidedPayLoadId = 10,
                        Origin = "DiffResult",
                        Data = "{\"Result\":\"Equal\",\"Diffs\":null}"
                    }
                };

                context.AddRange(list);
                context.SaveChanges();
            }            
        }

        public static bool AllMigrationsApplied(this DataAuthorityContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}

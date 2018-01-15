using DataAuthority.DataInfrastructure.DataModels;
using DataAuthority.SqlServerEF.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DataAuthority.SqlServerEF
{
    public class DataAuthorityContext : DbContext
    {        
        public DbSet<PayLoadDataModel> PayLoads { get; set; }

        public DataAuthorityContext(DbContextOptions<DataAuthorityContext> options)
            :base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DataAuthority;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PayLoadDataModelTypeConfiguration());
        }
    }
}

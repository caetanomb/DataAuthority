using DataAuthority.DataInfrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAuthority.SqlServerEF.EntityConfigurations
{
    public class PayLoadDataModelTypeConfiguration
        : IEntityTypeConfiguration<PayLoadDataModel>
    {
        public void Configure(EntityTypeBuilder<PayLoadDataModel> builder)
        {
            builder.ToTable("PayLoad");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProvidedPayLoadId)
                .IsRequired();
            builder.Property(p => p.Data)
                .IsRequired();
            builder.Property(p => p.Origin)
                .IsRequired();
        }
    }
}

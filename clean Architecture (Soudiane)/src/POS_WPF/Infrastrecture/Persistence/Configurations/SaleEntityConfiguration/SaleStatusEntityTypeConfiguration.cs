using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Clean_Architecture_Soufiane.Infrastructure.Persistence.Configurations
{
    class SaleStatusEntityTypeConfiguration
        : IEntityTypeConfiguration<SaleStatus>
    {
        public void Configure(EntityTypeBuilder<SaleStatus> saleStatusConfiguration)
        {
            saleStatusConfiguration.ToTable("SLE_STATUS");

            saleStatusConfiguration.HasKey(o => o.Id);

            saleStatusConfiguration.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            saleStatusConfiguration.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}

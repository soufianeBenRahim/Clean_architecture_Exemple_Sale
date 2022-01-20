using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Clean_Architecture_Soufiane.Infrastructure.Persistence.Confeguration
{
    class SaleStatusEntityTypeConfiguration
        : IEntityTypeConfiguration<SaleStatus>
    {
        public void Configure(EntityTypeBuilder<SaleStatus> orderStatusConfiguration)
        {
            orderStatusConfiguration.ToTable("SLE_STATUS");

            orderStatusConfiguration.HasKey(o => o.Id);

            orderStatusConfiguration.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            orderStatusConfiguration.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}

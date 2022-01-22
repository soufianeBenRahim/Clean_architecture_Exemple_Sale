using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Clean_Architecture_Soufiane.Infrastructure.Persistence.Confeguration
{
    class SaleEntityTypeConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> saleConfiguration)
        {
            saleConfiguration.ToTable("SLE_SALE");

            saleConfiguration.HasKey(o => o.Id);

            saleConfiguration.Ignore(b => b.DomainEvents);

            saleConfiguration.Property(o => o.Id)
                .UseHiLo("saleseq");

          


            saleConfiguration
                .Property<DateTime>("_saleDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SaleDate")
                .IsRequired();

            saleConfiguration
                .Property<int>("_saleStatusId")
                // .HasField("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SleStatusId")
                .IsRequired();

            

            saleConfiguration.Property<string>("Description").IsRequired(false);

            var navigation = saleConfiguration.Metadata.FindNavigation(nameof(Sale.SaleItems));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

          

      

            saleConfiguration.HasOne(o => o.SaleStatus)
                .WithMany()
                // .HasForeignKey("VenteStatusId");
                .HasForeignKey("_saleStatusId");
        }
    }
}

using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Clean_Architecture_Soufiane.Infrastructure.Persistence.Confeguration
{
    class SaleEntityTypeConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> orderConfiguration)
        {
            orderConfiguration.ToTable("SLE_SALE");

            orderConfiguration.HasKey(o => o.Id);

            orderConfiguration.Ignore(b => b.DomainEvents);

            orderConfiguration.Property(o => o.Id)
                .UseHiLo("saleseq");

          


            orderConfiguration
                .Property<DateTime>("_saleDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SaleDate")
                .IsRequired();

            orderConfiguration
                .Property<int>("_saleStatusId")
                // .HasField("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SleStatusId")
                .IsRequired();

            

            orderConfiguration.Property<string>("Description").IsRequired(false);

            var navigation = orderConfiguration.Metadata.FindNavigation(nameof(Sale.SaleItems));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

          

      

            orderConfiguration.HasOne(o => o.SaleStatus)
                .WithMany()
                // .HasForeignKey("VenteStatusId");
                .HasForeignKey("_saleStatusId");
        }
    }
}

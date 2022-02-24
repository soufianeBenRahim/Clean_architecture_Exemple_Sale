using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clean_Architecture_Soufiane.Infrastructure.Persistence.Confeguration
{
    class SaleItemEntityTypeConfiguration
        : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> saleItemConfiguration)
        {
            saleItemConfiguration.ToTable("SLE_ITEMS");

            saleItemConfiguration.HasKey(o => o.Id);

            saleItemConfiguration.Ignore(b => b.DomainEvents);

            saleItemConfiguration.Property<Guid>("SaleId")
                .IsRequired();

            saleItemConfiguration
                .Property<decimal>("_discount")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Discount")
                .IsRequired();

            saleItemConfiguration.Property<int>("ProductId")
                .IsRequired();

            saleItemConfiguration
                .Property<string>("_productName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ProductName")
                .IsRequired();

            saleItemConfiguration
                .Property<decimal>("_unitPrice")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UnitPrice")
                .IsRequired();

            saleItemConfiguration
                .Property<decimal>("_units")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Units")
                .IsRequired();

            saleItemConfiguration
                .Property<string>("_pictureUrl")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PictureUrl")
                .IsRequired(false);
        }
    }
}

using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using System;

namespace DeskTop.Unit.SaleAggregateTest
{
    public class OrderBuilder
    {
        private readonly Sale sale;

        public OrderBuilder()
        {
            sale = new Sale();
        }

        public OrderBuilder AddOne(
            Guid productId,
            string productName,
            decimal unitPrice,
            decimal discount,
            string pictureUrl,
            int units = 1)
        {
            sale.AddSaleItem(productId, productName, unitPrice, discount, pictureUrl, units);
            return this;
        }

        public Sale Build()
        {
            return sale;
        }
    }
}

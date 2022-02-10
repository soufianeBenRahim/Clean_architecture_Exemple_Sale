using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;

namespace DeskTop.Unit.SaleAggregateTest
{
    public class OrderBuilder
    {
        private readonly Sale order;

        public OrderBuilder()
        {
            order = new Sale();
        }

        public OrderBuilder AddOne(
            int productId,
            string productName,
            decimal unitPrice,
            decimal discount,
            string pictureUrl,
            int units = 1)
        {
            order.AddSaleItem(productId, productName, unitPrice, discount, pictureUrl, units);
            return this;
        }

        public Sale Build()
        {
            return order;
        }
    }
}

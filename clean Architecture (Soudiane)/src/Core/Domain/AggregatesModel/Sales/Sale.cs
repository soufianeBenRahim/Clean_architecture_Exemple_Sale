using Clean_Architecture_Soufiane.Domain.Events;
using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales
{
    
    
    public class Sale : AuditableEntity,  IAggregateRoot 
    {
        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        private DateTime _orderDate;

        public SaleStatus SaleStatus { get; private set; }
        private int _saleStatusId;


        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method OrderAggrergateRoot.AddOrderItem() which includes behaviour.
        private readonly List<SaleItem> _saleItems;
        public IReadOnlyCollection<SaleItem> SaleItems => _saleItems;

        public void SetSaleAsPaidCach(decimal amount)
        {
            this.SaleStatus = SaleStatus.Paid;
            this.AddDomainEvent(new SaleStatusChangedToPaidCachDomainEvent(Id));
        }
        public Sale()
        {
            _saleItems = new List<SaleItem>();
            _saleStatusId = SaleStatus.AwaitingValidation.Id;
            _orderDate = DateTime.UtcNow;

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            this.AddDomainEvent(new NewSaleDomainEvent(Id));
        }

       
        // DDD Patterns comment
        // This Order AggregateRoot's method "AddOrderitem()" should be the only way to add Items to the Order,
        // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
        // in order to maintain consistency between the whole Aggregate. 
        public void AddSaleItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, decimal units = 1)
        {
            var existingOrderForProduct = _saleItems.Where(o => o.ProductId == productId)
                .SingleOrDefault();

            if (existingOrderForProduct != null && existingOrderForProduct.UnitPrice== unitPrice)
            {
                //if previous line exist modify it with higher discount  and units..

                if (discount > existingOrderForProduct.Discount)
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }

                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                //add validated new order item

                var orderItem = new SaleItem(productId, productName, unitPrice, discount, pictureUrl, units);
                _saleItems.Add(orderItem);
            }
        }


        public decimal GetTotal()
        {
            return _saleItems.Sum(o => o.Units * o.UnitPrice);
        }

    }
}

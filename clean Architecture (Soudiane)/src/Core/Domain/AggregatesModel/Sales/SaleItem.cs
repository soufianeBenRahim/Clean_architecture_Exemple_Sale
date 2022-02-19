
using Clean_Architecture_Soufiane.Domain.Seedwork;
using Clean_Architecture_Soufiane.Domain.Exceptions;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales
{
    public class SaleItem
        : AuditableEntity
    {
        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        private string _productName;
        private string _pictureUrl;
        private decimal _unitPrice;
        private decimal _discount;
        private int _units;

        public int ProductId { get; private set; }

        protected SaleItem() { }

        public SaleItem(int productId, string productName, decimal unitPrice, decimal discount, string PictureUrl, int units = 1)
        {
            if (units <= 0)
            {
                throw new ValidationDomainException("Invalid number of units");
            }

            if ((unitPrice * units) < discount)
            {
                throw new ValidationDomainException("The total of order item is lower than applied discount");
            }

            ProductId = productId;

            _productName = productName;
            _unitPrice = unitPrice;
            _discount = discount;
            _units = units;
            _pictureUrl = PictureUrl;
        }


        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new ValidationDomainException("Discount is not valid");
            }

            _discount = discount;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new ValidationDomainException("Invalid units");
            }

            _units += units;
        }

        public string ProductName { get => _productName; private set=> _productName=value; }
        public int Units { get => _units; private set => _units = value; }
        public decimal UnitPrice { get => _unitPrice; private set => _unitPrice = value; }
        public decimal CurrentDiscount { get =>_discount; private set=> _discount=value; }
        public string PictureUri { get=>_pictureUrl; set=> _pictureUrl=value; }

        public decimal Discount { get => _discount; set => _discount = value; }
        
    }
}

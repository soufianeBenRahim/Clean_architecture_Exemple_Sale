

using Clean_Architecture_Soufiane.Domain.Seedwork;

namespace Clean_Architecture_Soufiane.Domain.Events
{
    public class SaleStatusChangedToPaidCachDomainEvent : DomainEvent
    {
        public SaleStatusChangedToPaidCachDomainEvent(int _idSale)
        {
            id = _idSale;
        }

        public int id { get; }
    }
}

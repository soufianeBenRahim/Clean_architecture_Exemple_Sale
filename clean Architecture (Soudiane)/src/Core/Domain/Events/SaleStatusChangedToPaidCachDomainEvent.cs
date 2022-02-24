

using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;

namespace Clean_Architecture_Soufiane.Domain.Events
{
    public class SaleStatusChangedToPaidCachDomainEvent : DomainEvent
    {
        public SaleStatusChangedToPaidCachDomainEvent(Guid _idSale)
        {
            id = _idSale;
        }

        public Guid id { get; }
    }
}

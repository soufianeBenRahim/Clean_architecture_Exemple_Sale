using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.Events
{
    public class NewSaleDomainEvent : DomainEvent
    {
        public NewSaleDomainEvent(int _idSale)
        {
            id = _idSale;
        }

        public int id { get; }
    }
}

using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.Events
{
    public class IthemAddToSaleDomainEvent : DomainEvent
    {
        public IthemAddToSaleDomainEvent(int _idSale,int _idIthem,int _NbUnit)
        {
            id = _idSale;
            idIthem = _idIthem;
            NbUnit = _NbUnit;
        }

        public int id { get; }
        public int idIthem { get; }
        public int NbUnit { get; }
    }

}

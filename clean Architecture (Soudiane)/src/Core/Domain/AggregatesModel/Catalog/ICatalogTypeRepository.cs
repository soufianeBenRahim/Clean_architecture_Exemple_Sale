using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog
{
    public interface ICatalogTypeRepository
    {
        IEnumerable<CatalogType> GetAll();
    }
}

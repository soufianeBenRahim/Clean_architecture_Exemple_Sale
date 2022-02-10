using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services.TesteServices
{
    public class FakeCatalogTypeRepository : ICatalogTypeRepository
    {
        private IEnumerable<CatalogType> _types = new List<CatalogType>()
            {
                new CatalogType() {Id=1,Type="type1" } ,
                new CatalogType() {Id=2,Type="type2" },
            };


        public IEnumerable<CatalogType> GetAll()
        {
            return _types;
        }
    }
}

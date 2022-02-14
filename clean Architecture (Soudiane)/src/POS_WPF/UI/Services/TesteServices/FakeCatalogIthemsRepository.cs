using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services.TesteServices
{
    public class FakeCatalogIthemsRepository : ICatalogIthemsRepository
    {
        private IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return ApplicationDbContextSeed.GetPreconfiguredItems();
        }
        public IEnumerable<CatalogItem> GetCatalogsByCatigoryId(int idType)
        {
            return GetPreconfiguredItems().Where(x => x.CatalogTypeId == idType).ToList();
        }

        public IEnumerable<CatalogItem> GetAll()
        {
            return GetPreconfiguredItems();
        }

        public IEnumerable<CatalogItem> GetItemByBarCode(string barCode)
        {
            return GetPreconfiguredItems().Where(x => x.Bar_Code == barCode).ToList();
        }
    }
}

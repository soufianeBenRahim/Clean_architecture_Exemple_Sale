using System.Collections.Generic;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog
{
    public interface ICatalogIthemsRepository 
    {
        IEnumerable<CatalogItem> GetCatalogsByCatigoryId(int idType);
        IEnumerable<CatalogItem> GetAll();
        IEnumerable<CatalogItem> GetItemByBarCode(string barCode);
        IEnumerable<CatalogItem> GetCatalogsByCatigoryIdAndName(int idType , string name);
    }
}
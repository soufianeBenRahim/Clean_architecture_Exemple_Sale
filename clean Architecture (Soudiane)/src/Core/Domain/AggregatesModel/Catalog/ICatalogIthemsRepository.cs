using System.Collections.Generic;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog
{
    public interface ICatalogIthemsRepository 
    {
        IEnumerable<CatalogItem> GetCatalogsByCatigoryId(int idType);
    }
}
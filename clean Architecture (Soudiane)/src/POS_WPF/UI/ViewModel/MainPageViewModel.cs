using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        ICatalogIthemsRepository _catalogeIthemsRepository;
        public MainPageViewModel(ICatalogTypeRepository typeRepository
            , ICatalogIthemsRepository catalogeIthemsRepository)
        {
            TypeCatalog = typeRepository.GetAll();
            _catalogeIthemsRepository = catalogeIthemsRepository;
        }
        public IEnumerable<CatalogType> TypeCatalog { get; internal set; }
        public IEnumerable<CatalogItem> CatalogsFiltred { get; set; }

        public void Filter(CatalogType catalogType)
        {
            CatalogsFiltred = _catalogeIthemsRepository.GetCatalogsByCatigoryId(catalogType.Id);
        }
    }
}

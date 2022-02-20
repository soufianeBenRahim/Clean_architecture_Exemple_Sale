using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using POS.Exceptions;
using POS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        ICatalogIthemsRepository _catalogeIthemsRepository;
        ISaleRepository _saleRepository;
        public MainPageViewModel(ICatalogTypeRepository typeRepository
            , ICatalogIthemsRepository catalogeIthemsRepository, ISaleRepository saleRepository)
        {
            TypeCatalog = typeRepository.GetAll();
            _catalogeIthemsRepository = catalogeIthemsRepository;
            CatalogsFiltred = _catalogeIthemsRepository.GetAll();
            this.LocalSal = new Sale();
            _saleRepository = saleRepository;
        }
        public ObservableCollection<SaleItem> SaleItems { get => new ObservableCollection<SaleItem>(this.LocalSal.SaleItems); }
        public IEnumerable<CatalogType> TypeCatalog { get; internal set; }

        public void Init()
        {
            LocalSal=new Sale();
            OnPropertyChanged("SaleItems");
        }

        public IEnumerable<CatalogItem> CatalogsFiltred { get; set; }
        public Sale LocalSal { get; set; }

        public void Filter(CatalogType catalogType)
        {
            if (catalogType == null)
            {
                CatalogsFiltred = _catalogeIthemsRepository.GetAll();
                return;
            }
            CatalogsFiltred = _catalogeIthemsRepository.GetCatalogsByCatigoryId(catalogType.Id);
        }

        public void AddItemToLocalSale(int productId,
            string productName,
            decimal unitPrice,
            decimal discount,
            string pictureUrl,
            int units = 1)
        {
            LocalSal.AddSaleItem( productId,
             productName,
             unitPrice,
             discount,
             pictureUrl,
             units = 1);
            _saleRepository.Add(LocalSal);
            _saleRepository.UnitOfWork.SaveChangesAsync();
            this.OnPropertyChanged("SaleItems");
        }

        public void ScanBarCode(string barCode)
        {
            if (string.IsNullOrEmpty(barCode))
            {
                return;
            }
            var items = _catalogeIthemsRepository.GetItemByBarCode(barCode);
            if (items == null || items.Count() == 0)
            {
                throw new BarCodeNotFondException();
            }
            if (items.Count() > 1)
            {
                var result = navigationServiceProxy.NavigateToAsync<ItemShooser>(new ItemShooserViewModel(items), (CurentView as FormeBase));
                if (result != null)
                {
                    var itemToAdd = result as CatalogItem;
                    AddItemToLocalSale(itemToAdd.Id,
                     itemToAdd.Name,
                     itemToAdd.Price,
                     0,
                     itemToAdd.PictureUri,
                     1);
                    Save();
                }

                return;
            }
            AddItemToLocalSale(items.First().Id,
                 items.First().Name,
                 items.First().Price,
                 0,
                 items.First().PictureUri,
                 1);
            Save();
        }

        private void Save()
        {
            _saleRepository.Add(LocalSal);
            _saleRepository.UnitOfWork.SaveChangesAsync();
        }

        public void FilterByName(CatalogType catalogType, string name)
        {
            if (catalogType == null && string.IsNullOrWhiteSpace(name))
            {
                CatalogsFiltred = _catalogeIthemsRepository.GetAll();
                return;
            }
            CatalogsFiltred = _catalogeIthemsRepository.GetCatalogsByCatigoryIdAndName(catalogType.Id, name);
        }

        public void ClearFilter()
        {
            CatalogsFiltred = _catalogeIthemsRepository.GetAll();
        }
    }
}

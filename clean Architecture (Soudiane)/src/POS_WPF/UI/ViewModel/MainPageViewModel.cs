using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using POS.Exceptions;
using POS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace POS.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        ICatalogIthemsRepository _catalogeIthemsRepository;
        ISaleRepository _saleRepository;
        public ObservableCollection<SaleItem> SaleItems { get => new ObservableCollection<SaleItem>(this.LocalSal.SaleItems); }
        public IEnumerable<CatalogType> TypeCatalog { get; internal set; }
        public IEnumerable<CatalogItem> CatalogsFiltred { get; set; }
        public Sale LocalSal { get; set; }

        CatalogType _selectedTypeCatalog;
        public CatalogType SelectedTypeCatalog 
        { 
            get => _selectedTypeCatalog; 
            set 
            { 
                _selectedTypeCatalog = value;
                Filter(value);
            } 
        }

        public MainPageViewModel(ICatalogTypeRepository typeRepository
            , ICatalogIthemsRepository catalogeIthemsRepository, 
            ISaleRepository saleRepository
            )
        {
            TypeCatalog = typeRepository.GetAll();
            _catalogeIthemsRepository = catalogeIthemsRepository;
            CatalogsFiltred = _catalogeIthemsRepository.GetAll();
            this.LocalSal = new Sale();
            _saleRepository = saleRepository;
        }


        public void Init()
        {
            LocalSal=new Sale();
            OnPropertyChanged("SaleItems");
        }



        public void Filter(CatalogType catalogType)
        {
            if (catalogType == null)
            {
                CatalogsFiltred = _catalogeIthemsRepository.GetAll();
                return;
            }
            CatalogsFiltred = _catalogeIthemsRepository.GetCatalogsByCatigoryId(catalogType.Id);
            OnPropertyChanged("CatalogsFiltred");
        }

        public void AddItemToLocalSale(CatalogItem item,decimal discount=0,  decimal units = 1)
        {
            if (units <= 0)
            {
                throw new QteInvalidException();
            }
            LocalSal.AddSaleItem(item.Id,
             item.Name,
             item.Price,
             discount,
            item.PictureUri,
             units);
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
                    AddItemToLocalSale(itemToAdd);
                }

                return;
            }
            AddItemToLocalSale(items.First());
        }


        public void FilterByName(string name)
        {
            if (SelectedTypeCatalog == null && !string.IsNullOrWhiteSpace(name))
            {
                CatalogsFiltred = _catalogeIthemsRepository.GetCatalogsByName( name);
                OnPropertyChanged("CatalogsFiltred");
                return;
            }
            if (SelectedTypeCatalog == null && string.IsNullOrWhiteSpace(name))
            {
                CatalogsFiltred = _catalogeIthemsRepository.GetAll();
                OnPropertyChanged("CatalogsFiltred");
                return;
            }
            CatalogsFiltred = _catalogeIthemsRepository.GetCatalogsByCatigoryIdAndName(SelectedTypeCatalog.Id, name);
            OnPropertyChanged("CatalogsFiltred");
        }

        public void ClearFilter()
        {
            CatalogsFiltred = _catalogeIthemsRepository.GetAll();
            OnPropertyChanged("CatalogsFiltred");
        }

        public void FilterByShourtCut(string shortCut)
        {
            var items = _catalogeIthemsRepository.FindByShourtCut(shortCut);
            if(items!=null && items.Count() == 1)
            {
                var first = items.ToList()[0];
                AddItemAfterAskedQte(first);
            }
            if(items != null && items.Count() > 1)
            {
                var result= navigationServiceProxy.NavigateToAsync<ItemShooser>(new ItemShooserViewModel(items), (CurentView as FormeBase));

                var item = result as CatalogItem;
                if (item != null)
                {
                    AddItemAfterAskedQte(item);
                }
            }
        }

        private void AddItemAfterAskedQte(CatalogItem item)
        {
            var resultQte = navigationServiceProxy.NavigateToAsync<QteDialog>(new QteDialogViewModel(), (CurentView as FormeBase));
            var qte = Convert.ToDecimal(resultQte);
            AddItemToLocalSale(item, 0, qte);
        }
    }
}

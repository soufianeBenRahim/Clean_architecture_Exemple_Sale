﻿using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
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
        public IEnumerable<CatalogType> TypeCatalog { get; internal set; }
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
        }

        public void ScanBarCode(string barCode)
        {
            if(string.IsNullOrEmpty(barCode))
            {
                return;
            }
            var items = _catalogeIthemsRepository.GetItemByBarCode(barCode);

                LocalSal.AddSaleItem(items.First().Id,
                 items.First().Name,
                 items.First().Price,
                 0,
                 items.First().PictureUri,
                 1);
                _saleRepository.Add(LocalSal);
                _saleRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}

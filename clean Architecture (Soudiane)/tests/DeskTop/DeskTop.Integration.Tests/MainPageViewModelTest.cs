using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using POS.Services;
using POS.Services.TesteServices;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using POS.View;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;

namespace DeskTop.Integration.MainPageViewModelTests
{
    public class MainPageViewModelTest : Testing
    {

        [Test]
        public void MainPage_WhenInt_ShouldLoadCatigoris()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            ICatalogTypeRepository catalogTypeRepository = ConfigurationService.getService<ICatalogTypeRepository>();
            var repositoryValue = catalogTypeRepository.GetAll();
            Assert.That(repositoryValue, Is.EqualTo(mainPage.TypeCatalog).Using(new CatalogTypeComparer()));
        }
        class CatalogTypeComparer : IEqualityComparer<CatalogType>
        {
            public bool Equals(CatalogType b1, CatalogType b2)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null || b2 == null)
                    return false;
                else if (b1.Id == b2.Id && b1.Type == b2.Type)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(CatalogType bx)
            {
                return bx.GetHashCode();
            }
        }


        [Test]
        public void MainPage_WhenSelectedCatigory_ShouldFilterCatalogesBySelectedCatigorie()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var catalogeIthemsRepository= ConfigurationService.getService<ICatalogIthemsRepository>();
            mainPage.Filter(new CatalogType() { Id = 2, Type = "type2" });
            var valuesrepository = catalogeIthemsRepository.GetCatalogsByCatigoryId(2);
            Assert.That(mainPage.CatalogsFiltred, Is.EqualTo(valuesrepository).Using(new CatalogIthemComparer()));
        }
        [Test]
        public void MainPage_WhenLoaded_ShouldloadAllCatalogItems()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var catalogeIthemsRepository = ConfigurationService.getService<ICatalogIthemsRepository>();
            var valuesrepository = catalogeIthemsRepository.GetAll();
            Assert.That(mainPage.CatalogsFiltred, Is.EqualTo(valuesrepository).Using(new CatalogIthemComparer()));
        }

        [Test]
        public void MainPage_WhenFilterWithNull_ShouldloadAllCatalogItems()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var catalogeIthemsRepository = ConfigurationService.getService<ICatalogIthemsRepository>();
            var valuesrepository = catalogeIthemsRepository.GetAll();
            mainPage.Filter(null);
            Assert.That(mainPage.CatalogsFiltred, Is.EqualTo(valuesrepository).Using(new CatalogIthemComparer()));
        }
      
        public class CatalogIthemComparer : IEqualityComparer<CatalogItem>
        {
            public bool Equals(CatalogItem b1, CatalogItem b2)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null || b2 == null)
                    return false;
                else if (b1.Id == b2.Id && b1.Name == b2.Name)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(CatalogItem bx)
            {
                return bx.GetHashCode();
            }
        }

        [Test]
        public async Task MainPage_AfterAddItemToLocalSale_ShouldSaveTheSaleInDataBase()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var SaleRepository = ConfigurationService.getService<ISaleRepository>();
            var item1=GetItemById(1);
            mainPage.AddItemToLocalSale(item1);
            var item2 = GetItemById(1);
            mainPage.AddItemToLocalSale(item2);
            var sale = await SaleRepository.GetAsync(mainPage.LocalSal.Id);
            Assert.That(mainPage.LocalSal.SaleItems, Is.EqualTo(sale.SaleItems).Using(new SaleIthemComparer()));
        }

        private static CatalogItem GetItemById(int id)
        {
           return ApplicationDbContextSeed.GetPreconfiguredItems().FirstOrDefault(x => x.Id == id);
        }

        [Test]
        public async Task MainPage_AfterAddTheSamItemToLocalSale_ShouldSaveTheSaleInDataBase()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var SaleRepository = ConfigurationService.getService<ISaleRepository>();
            var item1 = GetItemById(1);
            mainPage.AddItemToLocalSale(item1);
            mainPage.AddItemToLocalSale(item1);
            var sale = await SaleRepository.GetAsync(mainPage.LocalSal.Id);
            Assert.That(mainPage.LocalSal.SaleItems, Is.EqualTo(sale.SaleItems).Using(new SaleIthemComparer()));
        }
        [Test]
        public void MainPage_WhenSacanBarCode_ShouldAddTheCorespondingItemBarCodeToSaleLocal()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.IsBarCod=true;
            mainPage.ScanCode("1111");
            var item = mainPage.LocalSal.SaleItems.FirstOrDefault(x => x.Id == 1);
            Assert.NotNull(item);
        }
        class SaleIthemComparer : IEqualityComparer<SaleItem>
        {
            public bool Equals(SaleItem b1, SaleItem b2)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null || b2 == null)
                    return false;
                else if (b1.Id == b2.Id 
                    && b1.Discount == b2.Discount
                    && b1.ProductName == b2.ProductName
                    && b1.PictureUri == b2.PictureUri
                    && b1.UnitPrice == b2.UnitPrice
                    && b1.Units == b2.Units
                    && b1.ProductId == b2.ProductId)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(SaleItem bx)
            {
                return bx.GetHashCode();
            }
        }

        [Test]
        public void MainPage_DoFilterByName_ShouldFilterTheItemsCataloge()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var catalogeIthemsRepository = ConfigurationService.getService<ICatalogIthemsRepository>();
            mainPage.SelectedTypeCatalog = new CatalogType() { Id = 2, Type = "type2" };
            mainPage.FilterByName("cup");
            var valuesRepository = catalogeIthemsRepository.GetCatalogsByCatigoryIdAndName(2, "cup");
            Assert.That(mainPage.CatalogsFiltred, Is.EqualTo(valuesRepository).Using(new CatalogIthemComparer()));
        }
        [Test]
        public void MainPage_WhenDoClearFilter_ShouldloadAllCatalogItems()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var catalogeIthemsRepository = ConfigurationService.getService<ICatalogIthemsRepository>();
            var valuesrepository = catalogeIthemsRepository.GetAll();
            mainPage.ClearFilter();
            Assert.That(mainPage.CatalogsFiltred, Is.EqualTo(valuesrepository).Using(new CatalogIthemComparer()));
        }

    }
}

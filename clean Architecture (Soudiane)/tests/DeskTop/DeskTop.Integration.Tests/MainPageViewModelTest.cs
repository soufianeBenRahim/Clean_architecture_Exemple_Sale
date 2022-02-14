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
      
        class CatalogIthemComparer : IEqualityComparer<CatalogItem>
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
            mainPage.AddItemToLocalSale(1, "cup", 10.0m, 0, string.Empty);
            mainPage.AddItemToLocalSale(2, "cup", 20.0m, 0, string.Empty);
            var sale=await SaleRepository.GetAsync(mainPage.LocalSal.Id);
            Assert.That(mainPage.LocalSal.SaleItems, Is.EqualTo(sale.SaleItems).Using(new SaleIthemComparer()));
        }
        [Test]
        public async Task MainPage_AfterAddTheSamItemToLocalSale_ShouldSaveTheSaleInDataBase()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var SaleRepository = ConfigurationService.getService<ISaleRepository>();
            mainPage.AddItemToLocalSale(1, "cup", 10.0m, 0, string.Empty);
            mainPage.AddItemToLocalSale(1, "cup", 10.0m, 0, string.Empty);
            var sale = await SaleRepository.GetAsync(mainPage.LocalSal.Id);
            Assert.That(mainPage.LocalSal.SaleItems, Is.EqualTo(sale.SaleItems).Using(new SaleIthemComparer()));
        }
        [Test]
        public void MainPage_WhenSacanBarCode_ShouldAddTheCorespondingItemBarCodeToSaleLocal()
        {
            ConfigurationService.DataBaseSeed();
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.ScanBarCode("1111");
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
                    && b1.GetCurrentDiscount() == b2.GetCurrentDiscount()
                    && b1.GetOrderItemProductName() == b2.GetOrderItemProductName()
                    && b1.GetPictureUri() == b2.GetPictureUri()
                    && b1.GetUnitPrice() == b2.GetUnitPrice()
                    && b1.GetUnits() == b2.GetUnits()
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
            mainPage.FilterByName(new CatalogType() { Id = 2, Type = "type2" },"cup");
            var valuesRepository = catalogeIthemsRepository.GetCatalogsByCatigoryIdAndName(2, "cup");
            Assert.That(mainPage.CatalogsFiltred, Is.EqualTo(valuesRepository).Using(new CatalogIthemComparer()));
        }
    }
}

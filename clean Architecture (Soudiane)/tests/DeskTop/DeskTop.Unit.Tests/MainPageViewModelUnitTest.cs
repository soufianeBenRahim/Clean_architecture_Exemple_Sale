using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using DeskTop.Integration.Tests;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POS.Exceptions;
using POS.Services;
using POS.Services.TesteServices;
using POS.View;
using POS.ViewModel;
using System.Linq;

namespace DeskTop.Unit.Tests
{
    public class MainPageViewModelUnitTest
    {
        [OneTimeSetUp]
        public void setup()
        {
            var isMoke = true;
            var inMemoryDatabase = true;
            var services = new ServiceCollection();
            services.AddLogging();
            ConfigurationService.GetInstance(services, isMoke, inMemoryDatabase);
        }
        [Test]
        public void MainPage_WhenLoaded_ShouldInitializeTheLocalSaleObject()
        {
           
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            Assert.NotNull(mainPage.LocalSal);
        }
        [Test]
        public void MainPage_WhenBarCodeScanedIsNulOrEmpty_ShouldNotAddItemToSale()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.ScanBarCode("");
            Assert.IsEmpty(mainPage.LocalSal.SaleItems);
        }
        [Test]
        public void MainPage_WhenSacanBarCodeNotExiste_ShouldRaisNotFoundException()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            Assert.Throws<BarCodeNotFondException>(() => mainPage.ScanBarCode("123456789"));
        }
        [Test]
        public void MainPage_WhenScanBarCodeGetMultipleItem_ShouldShowTheItemShooserForm()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var navigationService = ConfigurationService.getService<INavigationService>();
            mainPage.ScanBarCode("1000");
            Assert.IsTrue(navigationService.getCurrent().Equals(typeof(ItemShooser)));
        }
        [Test]
        public void MainPage_WhenScanBarCodeGetMultipleItemAndUserNoSelect_ShouldNotAddItemToSale()
        {
           
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var countBefor=mainPage.LocalSal.SaleItems.Count;
            SetReteuRnedValueToFakeNavifationService(null);
            mainPage.ScanBarCode("1000");
            Assert.AreEqual(mainPage.LocalSal.SaleItems.Count, countBefor);
        }
        [Test]
        public void MainPage_WhenScanBarCodeGetMultipleItemAndUserSelectOne_ShouldAddItemToSale()
        {
            var mainPage = new MainPageViewModel(new FakeCatalogTypeRepository()
            , new FakeCatalogIthemsRepository(), new FackSaleRepository(new FackUnitOfWerk()));
            SetReteuRnedValueToFakeNavifationService(new CatalogItem());
            mainPage.ScanBarCode("1000");
            Assert.IsNotEmpty(mainPage.LocalSal.SaleItems);
        }

        private void SetReteuRnedValueToFakeNavifationService(CatalogItem value)
        {
            var navigationService = ConfigurationService.getService<INavigationService>();
            var Mock = navigationService as FakeNavigationProxy;
            Mock.RetunEdValue = value;
        }
        [Test]
        public void MainPage_WheneScanCodeBarrGetManyItemsAndOneIsSelectes_ShouldAddTenItemInTheSale()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var itemToAdd=ApplicationDbContextSeed.GetPreconfiguredItems().Where(x=>x.Id==2).First();
            SetReteuRnedValueToFakeNavifationService(itemToAdd);
            mainPage.ScanBarCode("1000");
            var itemAdd = mainPage.LocalSal.SaleItems.ToList()[0];
            Assert.AreEqual(itemAdd.ProductId, itemToAdd.Id);
        }
    }
}

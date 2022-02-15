using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POS.Exceptions;
using POS.Services;
using POS.View;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

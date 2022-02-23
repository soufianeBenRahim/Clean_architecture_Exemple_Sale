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
using System.Collections.Generic;
using System.ComponentModel;
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
            mainPage.Init();
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
        [Test]
        public void MainPage_WhenAddItemToSal_ShouldRaisPropertyChangeOfSaleItems()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var propertyName = "SaleItems";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.AddItemToLocalSale(1,"",10,0,"",1);
            Assert.IsTrue(isPropertyChanged);
            mainPage.PropertyChanged += null;
        }
        [Test]
        public void MainPage_WheneScanCodeBarrGetManyItemsAndOneIsSelectes_ShouldRaisPropertyChangeOfSaleItems()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var itemToAdd = ApplicationDbContextSeed.GetPreconfiguredItems().Where(x => x.Id == 2).First();
            SetReteuRnedValueToFakeNavifationService(itemToAdd);
            var propertyName = "SaleItems";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }

            };
            mainPage.ScanBarCode("1000");
            Assert.IsTrue(isPropertyChanged);
            mainPage.PropertyChanged += null;
        }
        [Test]
        public void MainPage_WhenClerSale_ShouldTheCollectionofSaleItemsEmpty()
        {

            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.AddItemToLocalSale(1, "", 2, 0, "");
            Assert.IsNotEmpty(mainPage.LocalSal.SaleItems);
            mainPage.Init();
            Assert.IsEmpty(mainPage.LocalSal.SaleItems);
        }

        [Test]
        public void MainPage_WhenClerSale_ShouldRaisPropertyChangeOfSaleItems()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.AddItemToLocalSale(1, "", 10, 0, "", 1);
            var propertyName = "SaleItems";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.Init();
            Assert.IsTrue(isPropertyChanged);
            mainPage.PropertyChanged += null;
        }
        [Test]
        public void MainPage_WhenSelectedCatalogTypeChange_ShouldFilterCataloge()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.SelectedTypeCatalog = new CatalogType() { Id = 2, Type = "dfsdf" };
            ICatalogIthemsRepository _catalogeIthemsRepository = ConfigurationService.getService<ICatalogIthemsRepository>();
            var CatalogsFiltred = _catalogeIthemsRepository.GetCatalogsByCatigoryId(2);
            Assert.That(CatalogsFiltred, Is.EqualTo(mainPage.CatalogsFiltred).Using(new CatalogIthemComparer()));
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
        public void MainPage_WhenSelectedCatalogTypeChange_ShouldRaesPropertyChangeToCatalogsFiltred()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
           
            var propertyName = "CatalogsFiltred";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.SelectedTypeCatalog = new CatalogType() { Id = 2, Type = "dfsdf" };
            Assert.IsTrue(isPropertyChanged);
        }
        [Test]
        public void MainPage_WhenClearFilterCatalogType_ShouldRaesPropertyChangeToCatalogsFiltred()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();

            var propertyName = "CatalogsFiltred";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.ClearFilter();
            Assert.IsTrue(isPropertyChanged);
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsByNameAndCatalogType_ShouldRaesPropertyChangeToCatalogsFiltred()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();

            var propertyName = "CatalogsFiltred";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.SelectedTypeCatalog = new CatalogType()
            {
                Id = 2,
                Type = "dsfdf"
            };
            mainPage.FilterByName(".Net");
            Assert.IsTrue(isPropertyChanged);
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsByNameAndSelectedTypeIsNul_ShouldShouldFiltedCatalogByNameOnly()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var catalogRepository= ConfigurationService.getService<ICatalogIthemsRepository>();
            var CatalogsFiltred = catalogRepository.GetCatalogsByName( ".Net");
            mainPage.SelectedTypeCatalog = null;
            mainPage.FilterByName(".Net");
            Assert.That(CatalogsFiltred, Is.EqualTo(mainPage.CatalogsFiltred).Using(new CatalogIthemComparer()));
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsByName_ShouldRaesPropertyChangeToCatalogsFiltred()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();

            var propertyName = "CatalogsFiltred";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.SelectedTypeCatalog = null;
            mainPage.FilterByName(".Net");
            Assert.IsTrue(isPropertyChanged);
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsAndNameAndCatalogTypeIsNull_ShouldRaesPropertyChangeToCatalogsFiltred()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();

            var propertyName = "CatalogsFiltred";
            bool isPropertyChanged = false; ;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.SelectedTypeCatalog = null;
            mainPage.FilterByName("");
            Assert.IsTrue(isPropertyChanged);
        }

        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndShourtCutNotExsite_ShouldNotAddItems()
        {

            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            mainPage.FilterByShourtCut("002");
            Assert.IsEmpty(mainPage.SaleItems);
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteOneItem_ShouldAddItemToLocalSle()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            mainPage.FilterByShourtCut("001");
            Assert.AreEqual(mainPage.SaleItems.Count,1);
        }
     
        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteManyItem_ShouldShowTheShooserForm()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            mainPage.FilterByShourtCut("003");
            var navigation=ConfigurationService.getService<INavigationService>();
            Assert.AreEqual(navigation.getCurrent(), typeof(ItemShooser));
        }

        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteManyItemAndOneIsSelected_ShouldAddThisItemToLocalSal()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            SetReteuRnedValueToFakeNavifationService(new CatalogItem() { Id = 1, Name = "exempel" });
            mainPage.FilterByShourtCut("003");
         
            Assert.IsNotEmpty(mainPage.SaleItems);
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteManyItemAndNoOneIsSelected_ShouldNotAddThisItemToLocalSal()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            SetReteuRnedValueToFakeNavifationService(null);
            mainPage.FilterByShourtCut("003");

            Assert.IsEmpty(mainPage.SaleItems);
        }

    }
}

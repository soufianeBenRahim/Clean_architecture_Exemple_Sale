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
            mainPage.IsBarCod = true;
            mainPage.ScanCode("");
            Assert.IsEmpty(mainPage.SaleItems);
        }
        [Test]
        public void MainPage_WhenSacanBarCodeNotExiste_ShouldRaisNotFoundException()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.IsBarCod = true;
            Assert.Throws<BarCodeNotFondException>(() => mainPage.ScanCode("123456789"));
        }
        [Test]
        public void MainPage_WhenScanBarCodeGetMultipleItem_ShouldShowTheItemShooserForm()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            InitNavigationAndSetReturnedValue();
            var navigationService = ConfigurationService.getService<INavigationService>();
            mainPage.IsBarCod = true;
            mainPage.ScanCode("1000");
            Assert.IsTrue(navigationService.getCurrent().Equals(typeof(ItemShooser)));
        }

        private void InitNavigationAndSetReturnedValue()
        {
            InitNavigation();
            var itemToAdd = ApplicationDbContextSeed.GetPreconfiguredItems().FirstOrDefault(x => x.Id == 2);
            SetReteuRnedValueToFakeNavifationService(itemToAdd);
            SetReteuRnedValueToFakeNavifationService(2m);
        }

        [Test]
        public void MainPage_WhenScanBarCodeGetMultipleItemAndUserNoSelect_ShouldNotAddItemToSale()
        {
           
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            InitNavigation();
            SetReteuRnedValueToFakeNavifationService(null);
            mainPage.IsBarCod = true;
            mainPage.ScanCode("1000");
            Assert.AreEqual(mainPage.SaleItems.Count, 0);
        }
        [Test]
        public void MainPage_WhenScanBarCodeGetMultipleItemAndUserSelectOne_ShouldAddItemToSale()
        {
            var mainPage = new MainPageViewModel(new FakeCatalogTypeRepository()
            , new FakeCatalogIthemsRepository(), new FackSaleRepository(new FackUnitOfWerk()));
            InitNavigationAndSetReturnedValue();
            mainPage.IsBarCod = true;
            mainPage.ScanCode("1000");
            Assert.AreEqual(mainPage.SaleItems.Count,1);
        }

        private void SetReteuRnedValueToFakeNavifationService(object value)
        {
            var navigationService = ConfigurationService.getService<INavigationService>() as FakeNavigationProxy;
            navigationService.RetunEdValue.Add(value);
        }
        [Test]
        public void MainPage_WheneScanCodeBarrGetManyItemsAndOneIsSelectes_ShouldAddTheItemInTheSale()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            InitNavigationAndSetReturnedValue();
            mainPage.IsBarCod = true;
            mainPage.ScanCode("1000");
            var itemAdd = mainPage.SaleItems.ToList()[0];
            Assert.AreEqual(itemAdd.ProductId, 2);
        }

        private static void InitNavigation()
        {
            var Navigation = ConfigurationService.getService<INavigationService>() as FakeNavigationProxy;
            Navigation.Init();
        }

        [Test]
        public void MainPage_WhenAddItemToSal_ShouldRaisPropertyChangeOfSaleItems()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            var propertyName = "SaleItems";
            bool isPropertyChanged = false;
            mainPage.PropertyChanged +=
            delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(propertyName))
                {
                    isPropertyChanged = true;
                }
            };
            mainPage.AddItemToLocalSale(GetItemById(1));
            Assert.IsTrue(isPropertyChanged);
            mainPage.PropertyChanged += null;
        }
        private static CatalogItem GetItemById(int id)
        {
            return ApplicationDbContextSeed.GetPreconfiguredItems().FirstOrDefault(x => x.Id == id);
        }
        [Test]
        public void MainPage_WheneScanCodeBarrGetManyItemsAndOneIsSelectes_ShouldRaisPropertyChangeOfSaleItems()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            InitNavigationAndSetReturnedValue();
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
            mainPage.IsBarCod = true;
            mainPage.ScanCode("1000");
            Assert.IsTrue(isPropertyChanged);
            mainPage.PropertyChanged += null;
        }
        [Test]
        public void MainPage_WhenClerSale_ShouldTheCollectionofSaleItemsEmpty()
        {

            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.AddItemToLocalSale(GetItemById(1));
            Assert.IsNotEmpty(mainPage.SaleItems);
            mainPage.Init();
            Assert.IsEmpty(mainPage.SaleItems);
        }

        [Test]
        public void MainPage_WhenClerSale_ShouldRaisPropertyChangeOfSaleItems()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.AddItemToLocalSale(GetItemById(1));
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
            mainPage.IsBarCod = false;
            mainPage.ScanCode("002");
            Assert.IsEmpty(mainPage.SaleItems);
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteOneItem_ShouldAddItemToLocalSaleWithToQteOfQteDialog()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            InitNavigation();
            SetReteuRnedValueToFakeNavifationService(2m);
            mainPage.IsBarCod = false;
            mainPage.ScanCode("001");
            Assert.AreEqual(mainPage.SaleItems[0].Units,2m);
        }
     
        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteManyItem_ShouldShowTheShooserFormAndQteDialogForm()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            InitNavigationAndSetReturnedValue();
            mainPage.IsBarCod = false;
            mainPage.ScanCode("003");
            var navigation=ConfigurationService.getService<INavigationService>() as FakeNavigationProxy;
            Assert.AreEqual(navigation.FomresStack[0], typeof(ItemShooser));
            Assert.AreEqual(navigation.FomresStack[1], typeof(QteDialog));
        }

        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteManyItemAndOneIsSelected_ShouldAddThisItemToLocalSalWithTeresultOfQteDialogForm()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            InitNavigationAndSetReturnedValue();
            mainPage.IsBarCod = false;
            mainPage.ScanCode("003");
         
            Assert.AreEqual(mainPage.SaleItems[0].Units,2);
        }
        [Test]
        public void MainPage_WhenFilterCatalogItemsByShourtCutAndExisteManyItemAndNoOneIsSelected_ShouldNotAddThisItemToLocalSal()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            InitNavigation();
            SetReteuRnedValueToFakeNavifationService(null);
            mainPage.IsBarCod = false;
            mainPage.ScanCode("003");
            Assert.IsEmpty(mainPage.SaleItems);
        }
        [Test]
        public void MainPage_WhenTrayToAddItemWitQteZiro_ShouldThrowQteInvalidException()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            Assert.Throws<QteInvalidException>(()=> mainPage.AddItemToLocalSale(GetItemById(1), 0,0)) ;
        }
        [Test]
        public void MainPage_WhenTrayToAddItemWitQteMinsThenZiro_ShouldThrowQteInvalidException()
        {
            var mainPage = ConfigurationService.getService<MainPageViewModel>();
            mainPage.Init();
            Assert.Throws<QteInvalidException>(() => mainPage.AddItemToLocalSale(GetItemById(1), 0, -1));
        }

    }
}

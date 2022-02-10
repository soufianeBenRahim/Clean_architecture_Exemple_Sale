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
    }
}

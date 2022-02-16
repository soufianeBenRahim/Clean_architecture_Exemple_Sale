using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POS;
using POS.Services;
using POS.Services.TesteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Unit.Tests
{
    public class ItemShooserViewModelUnitTest
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
        public void ItemSooserViewModel_whenInitialize_TheItemsViewEqualsItems()
        {
            var items = ApplicationDbContextSeed.GetPreconfiguredItems();
            var itemShouserViewModel = new ItemShooserViewModel(items);

            Assert.That(itemShouserViewModel.Items, Is.EqualTo(itemShouserViewModel.ItemsView).Using(new CatalogIthemComparer()));
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
        public void ItemSooserViewModel_WhenNotSelectedElement_shouldRereternedValueBeNull()
        {
            var items = ApplicationDbContextSeed.GetPreconfiguredItems();
            var itemShouserViewModel = new ItemShooserViewModel(items);
            Assert.IsNull(itemShouserViewModel.Result);
        }
        [Test]
        public void ItemSooserViewModel_whenSelectedElementChangedAndOkPressed_shooldFormClosedAndSetTheResultToSelectedItem()
        {
            var items = ApplicationDbContextSeed.GetPreconfiguredItems();
            var itemShouserViewModel = new ItemShooserViewModel(items);
            itemShouserViewModel.SetView(new FakeView());
            itemShouserViewModel.SelectedItem = items.ToList()[0];
            itemShouserViewModel.Ok();
            Assert.IsTrue(itemShouserViewModel.isFormClosed());
            Assert.IsNotNull(itemShouserViewModel.Result);
        }
        [Test]
        public void ItemSooserViewModel_whenFilter_TheneItemsViewEqualsTheFiltredElementOfItemsWithTeStringInFilterTextBox()
        {
            var items = ApplicationDbContextSeed.GetPreconfiguredItems();
            var itemShouserViewModel = new ItemShooserViewModel(items);
            var FilterText = "Cup";
            itemShouserViewModel.Filter(FilterText);
            Assert.That(itemShouserViewModel.ItemsView , Is.EqualTo(itemShouserViewModel.Items.Where(x=>x.Name.Contains(FilterText))).Using(new CatalogIthemComparer()));
        }

    }
}

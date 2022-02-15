using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using POS.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace POS
{
    public class ItemShooserViewModel : ViewModelBase
    {
        public ItemShooserViewModel(IEnumerable<CatalogItem> items)
        {
            Items = new ObservableCollection<CatalogItem>(items);
        }
        public ObservableCollection<CatalogItem> Items { get; set; }
    }
}
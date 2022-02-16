using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace POS
{
    public class ItemShooserViewModel : ViewModelBase
    {
        public ItemShooserViewModel(IEnumerable<CatalogItem> items)
        {
            Items = items;
            ItemsView = new ObservableCollection<CatalogItem>(items); 
        }
        public IEnumerable<CatalogItem> Items { get; set; }
        public CatalogItem SelectedItem { get=> Result as CatalogItem; set=> Result=value; }
        public object Result { get; set; }
        public ObservableCollection<CatalogItem> ItemsView { get; set; }

        public void Filter(string filterText)
        {
            ItemsView=new ObservableCollection<CatalogItem>(Items.Where(X=>X.Name.Contains(filterText)).ToList());
        }
    }
}
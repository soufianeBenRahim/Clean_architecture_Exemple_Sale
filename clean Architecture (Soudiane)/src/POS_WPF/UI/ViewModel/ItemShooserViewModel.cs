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
        private CatalogItem selectedItem;
        public CatalogItem SelectedItem { get=> selectedItem; set=> selectedItem = value; }
        
        public ObservableCollection<CatalogItem> ItemsView { get; set; }

        public void Filter(string filterText)
        {
            ItemsView=new ObservableCollection<CatalogItem>(Items.Where(X=>X.Name.Contains(filterText)).ToList());
        }

        public void Ok()
        {
            Result = SelectedItem;
            CurentView.CloseWindow();
        }
    }
}
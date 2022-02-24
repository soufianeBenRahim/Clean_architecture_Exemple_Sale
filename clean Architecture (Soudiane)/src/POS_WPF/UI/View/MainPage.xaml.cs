using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Microsoft.Extensions.DependencyInjection;
using POS.Exceptions;
using POS.Services;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POS.View
{
    /// <summary>
    /// Logique d'interaction pour MainPage.xaml
    /// </summary>
    public partial class MainPage : FormeBase
    {
        public MainPage(MainPageViewModel vm):base(vm)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = ConfigurationService.getService<LoginForm>();
            mainWindow.Owner = this;
            mainWindow.ShowDialog();
            codebarre.Focus();
        }

        private void codebarre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (IsBarCode.IsChecked.Value)
                    {
                        (_bctx as MainPageViewModel).ScanBarCode(codebarre.Text);
                    }
                    else
                    {
                        (_bctx as MainPageViewModel).FilterByShourtCut(codebarre.Text);
                    }
                }
                catch (BarCodeNotFondException ex)
                {
                    MessageBox.Show("Code barre n'existe pas !");
                }
                catch (QteInvalidException ex)
                {
                    MessageBox.Show("Quantité invalide !");
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Erreur non attendue !");                    
                }
                codebarre.Clear();
            }
        }

        private void clearTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            (_bctx as MainPageViewModel).ClearFilter();
            FiltreCataloge.Text = "";
        }

        private void FiltreCataloge_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                (_bctx as MainPageViewModel).FilterByName(FiltreCataloge.Text);
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            CatalogItem catalogItem = ((ListViewItem)sender).Content as CatalogItem;

            if (catalogItem != null)
            {
                (_bctx as MainPageViewModel).AddItemToLocalSale(catalogItem, 0);
            }
        }
    }
}

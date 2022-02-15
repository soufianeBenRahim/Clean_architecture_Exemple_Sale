using Microsoft.Extensions.DependencyInjection;
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
        }

    }
}

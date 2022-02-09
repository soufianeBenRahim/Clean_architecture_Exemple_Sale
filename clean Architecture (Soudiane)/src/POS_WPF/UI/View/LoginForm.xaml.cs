using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Clean_Architecture_Soufiane.Infrastructure.Repositories;
using Clean_Architecture_Soufiane.Infrastructure.Services;
using POS.Services;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLocalizeExtension.Engine;
using WPFLocalizeExtension.Providers;

namespace POS.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginForm : FormeBase
    {
        private LoginViewModel loginViewModel;
        public LoginForm(LoginViewModel vm):base(vm)
        {
            InitializeComponent();
            loginViewModel = vm;
        }
   
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo cultureInfo = (CultureInfo)comboLoc.SelectedItem;
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = cultureInfo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ILocalizationProvider provider = LocalizeDictionary.GetDefaultProvider(LocalizeDictionary.Instance);
            comboLoc.ItemsSource = provider.AvailableCultures;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.login(userName.Text,password.Password);
        }
    }
}

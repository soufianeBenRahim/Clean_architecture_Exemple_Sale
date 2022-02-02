using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Clean_Architecture_Soufiane.Infrastructure.Repositories;
using Clean_Architecture_Soufiane.Infrastructure.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginForm : Window
    {

        public LoginForm(LoginViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as LoginViewModel).login();
            this.Close();
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
             this.Close();
        }
        
    }
}

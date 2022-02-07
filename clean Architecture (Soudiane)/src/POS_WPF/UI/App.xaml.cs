using Clean_Architecture_Soufiane.Application;
using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Clean_Architecture_Soufiane.Infrastructure;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Clean_Architecture_Soufiane.Infrastructure.Services;
using Clean_Architecture_Soufiane.WPF;
using Microsoft.EntityFrameworkCore;
using Clean_Architecture_Soufiane.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using POS.View;
using POS.Services;
using POS.ViewModel;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using WPFLocalizeExtension.Engine;
using System.Globalization;

namespace POS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.CurrentCulture;
            ConfigurationService.GetInstance(false);
        }
     
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ConfigurationService.getService<MainPage>();
            mainWindow.Show();
        }
    }

}

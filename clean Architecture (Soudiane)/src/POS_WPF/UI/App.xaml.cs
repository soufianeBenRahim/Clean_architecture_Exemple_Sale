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
using Microsoft.Extensions.Logging;
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
using Microsoft.Extensions.Hosting;
using POS.Navigation;
using Microsoft.AspNetCore.Hosting;

namespace POS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly  IHost _host;
        public App()
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.CurrentCulture;
            _host = Host.CreateDefaultBuilder()
              .ConfigureLogging(builder => {
                  builder.ClearProviders();
                  builder.AddDebug();
              })            
              .ConfigureServices(services =>
              {
                  ConfigurationService.GetInstance(services, false);
                
              })
              .Build();
        }

        private static void DataBaseSeed()
        {
            try
            {
                IDbContextFactory<ApplicationDbContext> dbFactory = ConfigurationService.getService<IDbContextFactory<ApplicationDbContext>>();
        var SaleContext = dbFactory.CreateDbContext();

                if (SaleContext.Database.IsSqlite())
                {
                    SaleContext.Database.EnsureDeleted();
                    SaleContext.Database.EnsureCreated();
                }
           
                

                var logger =ConfigurationService.getService<ILogger<ApplicationDbContextSeed>>();

                var loggerSales = ConfigurationService.getService<ILogger<ApplicationDbContextSeed>>();
                new ApplicationDbContextSeed().SeedAsync(SaleContext, logger)
                                                   .Wait();

            }
            catch (Exception ex)
            {
                var logger = ConfigurationService.getService<ILogger<App>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {

            _host.Start();
            DataBaseSeed();
            MainWindow = ConfigurationService.getService<MainPage>();
            MainWindow.Show();

        }
      
    }

}

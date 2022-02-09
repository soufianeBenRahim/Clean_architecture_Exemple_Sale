using Clean_Architecture_Soufiane.Application;
using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Clean_Architecture_Soufiane.Infrastructure;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Clean_Architecture_Soufiane.Infrastructure.Repositories;
using Clean_Architecture_Soufiane.Infrastructure.Services;
using DeskTop.Integration.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POS.Navigation;
using POS.View;
using POS.ViewModel;
using System;
using System.IO;
using System.Windows;

namespace POS.Services
{
    public class ConfigurationService 
    {
        private static  ServiceProvider serviceProvider;

        public static void GetInstance(IServiceCollection services,bool isMock)
        {
            if (serviceProvider == null)
            {
                ConfigureServices(services,isMock);
                serviceProvider = services.BuildServiceProvider();
            }
        }
        public static T getService<T>()
        {
            return serviceProvider.GetService<T>();
        }
     
        private static  void ConfigureServices(IServiceCollection services,bool isMock)
        {
            if (isMock)
            {
                services.AddLogging();
                services.AddTransient<IDateTime, FakeDateTimeServcie>();
                services.AddSingleton<IUsersRepository, FakeUsersRepository>();
                services.AddSingleton<INavigationService, FakeNavigationProxy>();

                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<LoginForm>();
                services.AddSingleton<MainPage>();
            }
            else
            {
                services.AddDbContextFactory<ApplicationDbContext>(opt =>
                  opt.UseSqlite("Data Source = SaleDataBase.db"));

                services.AddInfrastructure();
                services.AddApplication();
                // infrastrecture dependancy injection
                services.AddSingleton<ICurrentUserService, CurrentUserServiceWPF>();
                services.AddSingleton<INavigationService, NavigationProxy>();
                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<LoginForm>();
                services.AddSingleton<MainPage>();
            }
     
            
        }
     
    }
}
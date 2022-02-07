using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Clean_Architecture_Soufiane.Infrastructure.Repositories;
using Clean_Architecture_Soufiane.Infrastructure.Services;
using DeskTop.Integration.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POS.View;
using POS.ViewModel;
using System;
using System.Windows;

namespace POS.Services
{
    public class ConfigurationService 
    {
        private static  ServiceProvider serviceProvider;
        public ConfigurationService()
        {

        }
        public static void GetInstance(bool isMock)
        {
            if (serviceProvider == null)
            {
                ServiceCollection services = new ServiceCollection();
                ConfigureServices(services, isMock);
                serviceProvider = services.BuildServiceProvider();
            }
        }
        public static T getService<T>()
        {
            return serviceProvider.GetService<T>();
        }
        private static  void ConfigureServices(ServiceCollection services, bool isMok)
        {
            if (isMok)
            {
                services.AddTransient<IDateTime, FakeDateTimeServcie>();
                services.AddSingleton<IUsersRepository, FakeUsersRepository>();
                services.AddSingleton<INavigationService, FakeNavigationProxy>();
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite("Data Source = SaleDataBase.db");
                });

                // infrastrecture dependancy injection
                services.AddScoped<IDomainEventService, DomainEventService>();
                services.AddTransient<IDateTime, DateTimeService>();
                services.AddScoped<ISaleRepository, SaleRepository>();

                services.AddSingleton<ICurrentUserService, CurrentUserServiceWPF>();
                services.AddSingleton<IDateTime, DateTimeService>();
                services.AddSingleton<IUsersRepository, UsersRepository>();
            }

            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<LoginForm>();
            services.AddSingleton<MainPage>();
        }
     
    }
}
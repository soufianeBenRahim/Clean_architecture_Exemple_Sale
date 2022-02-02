﻿using Clean_Architecture_Soufiane.Application;
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

namespace POS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        public IConfiguration Configuration { get; }
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
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
            services.AddSingleton<IDateTime,DateTimeService>();
            services.AddSingleton<INavigationService, NavigationServiceFabrique>();
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<LoginForm>();
            services.AddSingleton<MainPage>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<LoginForm>();
            mainWindow.Show();
        }
    }

}

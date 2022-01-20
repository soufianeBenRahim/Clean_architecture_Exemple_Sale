using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var SaleContext = services.GetRequiredService<ApplicationDbContext>();

                    if (SaleContext.Database.IsSqlServer())
                    {
                        SaleContext.Database.EnsureDeleted();
                        SaleContext.Database.EnsureCreated();
                    }


                    var env = services.GetService<IWebHostEnvironment>();
                    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();

                     var loggerSales = services.GetService<ILogger<ApplicationDbContextSeed>>();
                    new ApplicationDbContextSeed().SeedAsync(SaleContext, env,true, logger)
                                                       .Wait();

                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

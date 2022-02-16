using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Unit.Tests
{
    public  class ConfigurationServiceUnitTest
    {
        [Test]
        public void ConfigurationService_WheneCallGetService_ShouldeReturneTheRegistredService()
        {
            var isMoke = true;
            var inMemoryDatabase = true;
            var services = new ServiceCollection();
            services.AddLogging();
            ConfigurationService.GetInstance(services, isMoke, inMemoryDatabase);
            var Navigation = ConfigurationService.getService<INavigationService>();
            Assert.IsNotNull(Navigation);
        }
    }
}

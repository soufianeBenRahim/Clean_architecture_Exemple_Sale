using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POS;
using POS.Services;
using POS.Services.TesteServices;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Unit.Tests
{
    public class QteDialogViewModelUnitTest
    {
        [OneTimeSetUp]
        public void setup()
        {
            var isMoke = true;
            var inMemoryDatabase = true;
            var services = new ServiceCollection();
            services.AddLogging();
            ConfigurationService.GetInstance(services, isMoke, inMemoryDatabase);
        }
        [Test]
        public void QteDialogViewModel_WhenNonValidateDialog_ShouldReturnNull()
        {
            var viewModel = new QteDialogViewModel();
            Assert.IsNull(viewModel.Result);
        }

        [Test]
        public void QteDialogViewModel_WhenValidateDialog_ShouldCloseForme()
        {
            var viewModel = new QteDialogViewModel();
            viewModel.SetView(new FakeView());
            viewModel.Ok();
            Assert.IsTrue(viewModel.isFormClosed());
        }
        [Test]
        public void QteDialogViewModel_WhenIsIntialise_ShouldQteEqualsOne()
        {
            var viewModel = new QteDialogViewModel();
            viewModel.SetView(new FakeView());
            Assert.AreEqual(viewModel.Qte, 1m);
        }
        [Test]
        public void QteDialogViewModel_WhenValidateDialog_ShouldSetTheResultToQteProperty()
        {
            var viewModel = new QteDialogViewModel();
            viewModel.SetView(new FakeView());
            viewModel.Qte = 2m;
            viewModel.Ok();
            Assert.AreEqual(Convert.ToDecimal(viewModel.Result),2m);
        }
    }
}

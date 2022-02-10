using NUnit.Framework;
using POS.ViewModel;
using System.Windows;
using POS.View;
using POS.Services;
using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using POS.Services.TesteServices;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace DeskTop.Integration.LoginViewModelTests
{
    public class LoginViewModelTest: Testing
    {

        public LoginViewModelTest()
        {
         
        }

        [Test]
        public  void LoginViewModel_WhenPasswordIsHeurAtMinut_ShouldGoToTheMainPage()
        {
            var dateTimeService = ConfigurationService.getService<IDateTime>();
            var loginViewModel = ConfigurationService.getService<LoginViewModel>();
            var pass =$"{dateTimeService.Now.Hour}@{dateTimeService.Now.Minute}";
            loginViewModel.SetView(new FakeView());
            loginViewModel.login("soufiane", pass);
            Assert.IsTrue(loginViewModel.isFormClosed());
        }
        [Test]
        public void LoginViewModel_WhenUserNameAndPasswerdExisteInUserTtabele_ShouldGoToTheMainPage()
        {
            ConfigurationService.RestartDataBase();
            var loginViewModel = ConfigurationService.getService<LoginViewModel>();
            IUsersRepository userRepository = ConfigurationService.getService<IUsersRepository>();
            userRepository.AddUser("Soufiane", "1234");
            loginViewModel.SetView(new FakeView());
            loginViewModel.login("Soufiane", "1234");
            Assert.IsTrue(loginViewModel.isFormClosed());

        }
        [Test]
        public void LoginViewModel_WhenUserNameAndPasswerdNotExisteInUserTtabele_ShouldNotGoToClosed()
        {
            ConfigurationService.RestartDataBase();
            var loginViewModel = ConfigurationService.getService<LoginViewModel>();
            IUsersRepository userRepository = ConfigurationService.getService<IUsersRepository>();
            userRepository.AddUser("Soufiane", "1234");
            loginViewModel.SetView(new FakeView());
            loginViewModel.login("Soufiane", "1212");
            Assert.IsFalse(loginViewModel.isFormClosed());

        }

    }
}

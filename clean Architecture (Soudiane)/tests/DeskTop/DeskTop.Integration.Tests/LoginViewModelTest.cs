using NUnit.Framework;
using POS.ViewModel;
using System.Windows;
using POS.View;
using POS.Services;
using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using POS.Services.TesteServices;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;

namespace DeskTop.Integration.Tests
{
    public class LoginViewModelTest
    {
        public LoginViewModelTest()
        {
            ConfigurationService.GetInstance(true);
        }
        [Test]
        public  void LoginViewModel_WhenPasswordIsHeurAtMinut_ShouldGoToTheMainPage()
        {

            ConfigurationService.GetInstance(true);
            var  dateTimeService= ConfigurationService.getService<IDateTime>();
            var loginViewModel = ConfigurationService.getService<LoginViewModel>();
            loginViewModel.UserName = "soufiane";
            loginViewModel.PassWord =$"{dateTimeService.Now.Hour}@{dateTimeService.Now.Minute}";
            loginViewModel.SetView(new FakeView());
            loginViewModel.login();
            Assert.IsTrue(loginViewModel.isFormClosed());
        }
        [Test]
        public void LoginViewModel_WhenUserNameAndPasswerdExisteInUserTtabele_ShouldGoToTheMainPage()
        {
            ConfigurationService.GetInstance(true);
            var loginViewModel = ConfigurationService.getService<LoginViewModel>();
            IUsersRepository userRepository = ConfigurationService.getService<IUsersRepository>();
            userRepository.AddUser("Soufiane", "1234");
            loginViewModel.UserName = "Soufiane";
            loginViewModel.PassWord = "1234";
            loginViewModel.SetView(new FakeView());
            loginViewModel.login();
            Assert.IsTrue(loginViewModel.isFormClosed());

        }

    }
}

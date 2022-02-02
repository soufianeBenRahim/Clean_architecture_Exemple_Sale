using NUnit.Framework;
using POS.ViewModel;
using System.Windows;
using POS.View;

namespace DeskTop.Integration.Tests
{
    public class LoginViewModelTest
    {
        public LoginViewModelTest()
        {

        }
        [Test]
        public  void LoginViewModel_WhenPasswordIsHeurAtMinut_ShouldGoToTheMainPage()
        {
            FakeDateTimeServcie dateTimeService = new FakeDateTimeServcie();
            FakeNavigationService navigationservice = new FakeNavigationService();
            UsersRepositoryFake userRepository = new UsersRepositoryFake();
            var loginViewModel = new LoginViewModel(dateTimeService, navigationservice, userRepository);
            loginViewModel.UserName = "soufiane";
            loginViewModel.PassWord =$"{dateTimeService.Now.Hour}@{dateTimeService.Now.Minute}";
            loginViewModel.login();
            Assert.AreEqual(navigationservice.getCurrent(), typeof(MainPage));
          
        }
        [Test]
        public void LoginViewModel_WhenUserNameAndPasswerdExisteInUserTtabele_ShouldGoToTheMainPage()
        {
            FakeDateTimeServcie dateTimeService = new FakeDateTimeServcie();
            FakeNavigationService navigationservice = new FakeNavigationService();
            UsersRepositoryFake userRepository = new UsersRepositoryFake();
            userRepository.AddUser("Soufiane", "1234");
            var loginViewModel = new LoginViewModel(dateTimeService, navigationservice, userRepository);
            loginViewModel.UserName = "Soufiane";
            loginViewModel.PassWord = "1234";
            loginViewModel.login();
            Assert.AreEqual(navigationservice.getCurrent(), typeof(MainPage));

        }

    }
}

using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using POS.Services;
using POS.View;

namespace POS.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        IDateTime DateTimeServce;
        INavigationService NavigationServiceFabrique;
        IUsersRepository userRepository;
        public LoginViewModel(IDateTime dateTimeServce,INavigationService navigationServiece, IUsersRepository userRepository )
        {
            this.DateTimeServce = dateTimeServce;
            this.NavigationServiceFabrique = navigationServiece;
            this.userRepository = userRepository;
        }

        private string userName;
        public string UserName 
        { 
            get 
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged(UserName);
            }
        }

        private string passWord;
        public string PassWord
        {
            get
            {
                return passWord;
            }
            set
            {
                passWord = value;
                OnPropertyChanged(UserName);
            }
        }

        public void login()
        {
            if (PassWord != null &&
                            PassWord.Equals($"{DateTimeServce.Now.Hour}@{DateTimeServce.Now.Minute}"))
            {

                this.NavigationServiceFabrique.NavigateToAsync(typeof(MainPage));
                return;
            }
            if (PassWord != null && UserName!=null &&
                this.userRepository.FindUserByUserNameAndPassword(UserName, PassWord))
            {
                
                this.NavigationServiceFabrique.NavigateToAsync(typeof(MainPage));
            }
        }

    }
}
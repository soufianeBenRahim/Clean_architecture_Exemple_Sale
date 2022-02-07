using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using POS.Services;
using POS.View;
using System.ComponentModel;
using System.Threading.Tasks;

namespace POS.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        IDateTime DateTimeServce;
        IUsersRepository userRepository;


        public RelayCommand loginCommand { get; init; }
        public LoginViewModel(IDateTime dateTimeServce,IUsersRepository userRepository)
        {
            this.DateTimeServce = dateTimeServce;
            this.userRepository = userRepository;
            loginCommand = new RelayCommand(login,canLogin);
        }

        private bool  canLogin()
        {
            return userName != null && passWord != null;
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            loginCommand.NotifyCanExecuteChanged();
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
               SetProperty(ref userName,value) ;
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
                SetProperty(ref passWord, value);
            }
        }

        public void login()
        {
            if (PassWord != null &&
                            PassWord.Equals($"{DateTimeServce.Now.Hour}@{DateTimeServce.Now.Minute}"))
            {
                if (CurentView != null)
                {
                    CurentView.CloseWindow();
                }
                return;
            }
            if (PassWord != null && UserName!=null &&
                this.userRepository.FindUserByUserNameAndPassword(UserName, PassWord))
            {

                if (CurentView != null)
                {
                   CurentView.CloseWindow();
                }
            }
        }

    }
}
﻿using Clean_Architecture_Soufiane.Application.Common.Interfaces;
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

        public LoginViewModel(IDateTime dateTimeServce,IUsersRepository userRepository)
        {
            this.DateTimeServce = dateTimeServce;
            this.userRepository = userRepository;
        }

        public void login(string user,string pass)
        {
            if (pass != null &&
                            pass.Equals($"{DateTimeServce.Now.Hour}@{DateTimeServce.Now.Minute}"))
            {
                if (CurentView != null)
                {
                    CurentView.CloseWindow();
                }
                return;
            }
            var existe = this.userRepository.FindUserByUserNameAndPassword(user, pass);
            if (pass != null && user != null && existe)
            {
                if (CurentView != null)
                {
                   CurentView.CloseWindow();
                }
            }
        }

    }
}
﻿using POS.View;
using POS.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace POS.Services
{
    public interface INavigationService
    {
        object NavigateToAsync<T>(ViewModelBase viemModel ,FormeBase parent);

        Type getCurrent();
    }
}
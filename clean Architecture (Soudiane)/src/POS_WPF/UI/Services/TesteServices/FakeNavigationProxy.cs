using POS.Services;
using POS.View;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeskTop.Integration.Tests
{
    public class FakeNavigationProxy:INavigationService
    {
        private Type current;
        public Type getCurrent()
        {
            return  current;
        }

        public void NavigateToAsync<T>(ViewModelBase viemModel, FormeBase parent)
        {
            current = typeof(T);
        }
    }
}

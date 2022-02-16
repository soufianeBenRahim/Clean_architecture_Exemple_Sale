using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
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

        public object RetunEdValue { get; set; }

        public Type getCurrent()
        {
            return  current;
        }

        public object NavigateToAsync<T>(ViewModelBase viemModel, FormeBase parent)
        {
            current = typeof(T);
            return RetunEdValue;
        }
    }
}

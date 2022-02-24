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
    public class FakeNavigationProxy : INavigationService
    {
        private Type current;
        public List<Type> FomresStack { get; set; }
        public int CurrentIndex { get; set; }
        public List<object> RetunEdValue { get; set; }
        public void Init ()
        {
            FomresStack =new List<Type>();
            CurrentIndex = -1;
            RetunEdValue=new List<object>();
        }
        public Type getCurrent()
        {
            return  current;
        }

        public object NavigateToAsync<T>(ViewModelBase viemModel, FormeBase parent)
        {
            current = typeof(T);
            FomresStack.Add(typeof(T));
            CurrentIndex++;
            return RetunEdValue[CurrentIndex];
        }
    }
}

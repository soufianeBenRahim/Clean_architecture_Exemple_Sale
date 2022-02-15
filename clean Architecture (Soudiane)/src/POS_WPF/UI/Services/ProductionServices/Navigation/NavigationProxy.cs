using POS.Services;
using POS.View;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POS.Navigation
{
    public class NavigationProxy : INavigationService
    {
        private Type CurentForme;
        public void NavigateToAsync<T>(ViewModelBase viemModel, FormeBase parent)
        {
            var page = (T)Activator.CreateInstance(typeof(T), viemModel); ;

            if (page != null)
            {
                var pageToOpen = page as FormeBase;
                if (pageToOpen != null)
                {
                    pageToOpen.Owner = parent;
                    pageToOpen.ShowDialog();
                    CurentForme = pageToOpen.GetType();
                }
            }
        }

        public Type getCurrent()
        {
            return CurentForme;
        }
    }
}

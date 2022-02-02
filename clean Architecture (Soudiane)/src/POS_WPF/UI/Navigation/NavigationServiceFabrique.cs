using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace POS.Services
{
    public class NavigationServiceFabrique : INavigationService
    {
        public NavigationServiceFabrique()
        {

        }

        Type CurentForme;
        public void NavigateToAsync(Type forme)
        {
            if(forme==null)
                return;
            Window page = Activator.CreateInstance(forme) as Window;
            if (page != null)
            {
                page.Show();
                 CurentForme = forme;
            }

        }
    
        public Type getCurrent()
        {
            return CurentForme;
        }
    }
}
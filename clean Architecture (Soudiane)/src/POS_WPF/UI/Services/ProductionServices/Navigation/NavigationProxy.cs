using POS.Services;
using POS.View;
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
        public void NavigateToAsync<T>()
        {
            var page = ConfigurationService.getService<T>();

            if (page != null)
            {
                var pageToOpen = page as FormeBase;
                if (pageToOpen != null)
                {
                    pageToOpen.IsClosed = false;
                    Application.Current.MainWindow = pageToOpen;
                    pageToOpen.Show();
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

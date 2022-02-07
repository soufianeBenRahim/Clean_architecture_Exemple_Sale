using System;
using System.Threading.Tasks;
using System.Windows;

namespace POS.Services
{
    public interface INavigationService
    {
        void NavigateToAsync<T>();

        Type getCurrent();
    }
}
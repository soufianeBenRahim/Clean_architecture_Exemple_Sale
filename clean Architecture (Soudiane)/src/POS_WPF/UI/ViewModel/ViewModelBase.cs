using Microsoft.Toolkit.Mvvm.ComponentModel;
using POS.Services;
using POS.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace POS.ViewModel
{
    public class ViewModelBase : ObservableObject
    {
        internal INavigationService navigationServiceProxy;
        internal IView CurentView;
        public ViewModelBase()
        {
            navigationServiceProxy = ConfigurationService.getService<INavigationService>();
        }
        public void SetView(IView view)
        {
            CurentView = view;
        }
        public bool isFormClosed()
        {
            return CurentView.IsClosed;
        }
        public object Result { get; set; }
    }
}

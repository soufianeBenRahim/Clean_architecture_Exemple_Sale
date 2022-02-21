using POS.Services;
using POS.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace POS.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        internal INavigationService navigationServiceProxy;
        internal IView CurentView;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
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

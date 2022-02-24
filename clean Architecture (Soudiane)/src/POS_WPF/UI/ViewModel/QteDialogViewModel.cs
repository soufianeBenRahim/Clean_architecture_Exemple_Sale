using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModel
{
    public class QteDialogViewModel : ViewModelBase
    {
        private decimal _qte;
        public decimal Qte { get=>_qte; 
            set { _qte = value;OnPropertyChanged("Qte"); } }

        public void Ok()
        {
            this.Result = Qte;
            CurentView.CloseWindow();
        }
    }
}

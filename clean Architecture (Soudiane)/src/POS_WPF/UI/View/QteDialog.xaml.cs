using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POS.View
{
    /// <summary>
    /// Interaction logic for QteDialog.xaml
    /// </summary>
    public partial class QteDialog : FormeBase
    {
        public QteDialog(QteDialogViewModel vm):base(vm) 
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            (_bctx as QteDialogViewModel).Ok();
        }

        private void FormeBase_Loaded(object sender, RoutedEventArgs e)
        {
            Qte.Focus();
            Qte.SelectAll();
        }
    }
}

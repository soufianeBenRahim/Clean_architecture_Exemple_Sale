using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.View
{
    public interface IView
    {
        void CloseWindow();
        bool IsClosed { get; set; }
       
    }
}

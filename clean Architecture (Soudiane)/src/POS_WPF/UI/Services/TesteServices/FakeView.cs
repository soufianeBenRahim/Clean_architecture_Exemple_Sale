using POS.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services.TesteServices
{
    public class FakeView : IView
    {
        private bool isClosed = false;

        public bool IsClosed
        {
            get
            {
                return this.isClosed;
            }
            set
            {
                this.isClosed = value;
            }
        }

        public void CloseWindow()
        {
            isClosed=true;
        }
    }
}

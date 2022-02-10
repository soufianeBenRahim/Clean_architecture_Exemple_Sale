﻿using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POS.View
{
    public class FormeBase:Window,IView
    {
        private bool isClosed = false;
        public FormeBase(ViewModelBase vm )
        {
            this.DataContext = vm;
            vm.SetView(this);
        }
        
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
            isClosed = true;
            this.Close();
        }
    }
}
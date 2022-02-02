﻿using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeskTop.Integration.Tests
{
    public class FakeNavigationService : INavigationService
    {
        private Type current;
        public Type getCurrent()
        {
            return  current;
        }

        public void NavigateToAsync(Type Forme)
        {
            current= Forme;
        }
    }
}

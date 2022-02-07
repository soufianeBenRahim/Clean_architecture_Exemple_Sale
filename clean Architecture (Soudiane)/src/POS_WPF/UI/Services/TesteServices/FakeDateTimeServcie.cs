using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Integration.Tests
{
    public class FakeDateTimeServcie : IDateTime
    {
        public DateTime Now => new DateTime(2022,1,1);
    }
}

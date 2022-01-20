using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using System;

namespace Clean_Architecture_Soufiane.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

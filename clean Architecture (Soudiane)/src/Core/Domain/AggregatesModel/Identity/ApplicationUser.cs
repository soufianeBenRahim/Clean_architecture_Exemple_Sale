using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity
{
    public class ApplicationUser :  IAggregateRoot 
    {
        public Guid ID { get; set; }
        public string  UserName { get; set; }
        public string Password { get; set; }
    
    }
}

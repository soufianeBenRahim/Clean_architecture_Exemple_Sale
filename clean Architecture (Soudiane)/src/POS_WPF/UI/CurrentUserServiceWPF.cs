using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class CurrentUserServiceWPF : ICurrentUserService
    {
        public string UserId => "ADMIN";
    }
}

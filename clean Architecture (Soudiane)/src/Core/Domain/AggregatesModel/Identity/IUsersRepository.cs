using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity
{
    public interface IUsersRepository : IRepository<ApplicationUser>
    {
        void AddUser(string userName, string password);
        bool FindUserByUserNameAndPassword(string userName, string password);
    }
}

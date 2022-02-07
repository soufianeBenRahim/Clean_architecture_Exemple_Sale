using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Integration.Tests
{
    public class FakeUsersRepository : IUsersRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
        private List<ApplicationUser> users= new List<ApplicationUser>();
        public bool FindUserByUserNameAndPassword(string userName, string password)
        {
            if (users != null)
            {
                return users.Any(x => x.UserName == userName && x.Password == password);
            }
            else
            {
                return false;
            }
        }

        public void AddUser(string userName, string password)
        {
            if (users == null)
                users = new List<ApplicationUser>();
            users.Add(new ApplicationUser()
            {
                ID = Guid.NewGuid(),
                UserName="Soufiane",
                Password="1234",
            });

        }
    }
}

using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Clean_Architecture_Soufiane.Domain.Seedwork;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public void AddUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool FindUserByUserNameAndPassword(string userName, string password)
        {
            return true;
        }
    }
}

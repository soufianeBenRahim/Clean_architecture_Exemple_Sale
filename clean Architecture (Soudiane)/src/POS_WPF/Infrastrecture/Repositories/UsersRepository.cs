using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;
using Clean_Architecture_Soufiane.Domain.Seedwork;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._dbFactory.CreateDbContext();
            }
        }
        public UsersRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }
        public async void AddUser(string userName, string password)
        {
            using (ApplicationDbContext db = this._dbFactory.CreateDbContext())
            {
                await  db.Users.AddAsync(new ApplicationUser() { ID = Guid.NewGuid(), Password = password, UserName = userName });
                await db.SaveChangesAsync();
            }
                
        }

        public bool FindUserByUserNameAndPassword(string userName, string password)
        {
            using (ApplicationDbContext db = this._dbFactory.CreateDbContext())
            {
                var existe = db.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
                var users = db.Users.ToList();
                return existe!=null;
            }
        }
    }
}

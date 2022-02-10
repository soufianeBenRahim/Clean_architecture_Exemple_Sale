using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Infrastructure.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public CatalogTypeRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public IEnumerable<CatalogType> GetAll()
        {
            using (var dbContext=_dbFactory.CreateDbContext())
            {
               return  dbContext.CatalogTypes.ToList();
            }
        }

      
    }
}

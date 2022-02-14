using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture_Soufiane.Infrastructure.Repositories
{
    public class CatalogIthemsRepository: ICatalogIthemsRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public CatalogIthemsRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IEnumerable<CatalogItem> GetAll()
        {
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                return dbContext.CatalogItems.ToList();
            }
        }

        public IEnumerable<CatalogItem> GetCatalogsByCatigoryId(int idType)
        {
            using(var dbContext= _dbFactory.CreateDbContext())
            {
                return dbContext.CatalogItems.Where(x => x.CatalogTypeId == idType).ToList();
            }
        }

        public IEnumerable<CatalogItem> GetItemByBarCode(string barCode)
        {
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                return dbContext.CatalogItems.Where(x => x.Bar_Code == barCode).ToList();
            }
        }
    }
}
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture_Soufiane.Infrastructure.Repositories
{
    public class CatalogIthemsRepository: ICatalogIthemsRepository
    {
        private readonly ApplicationDbContext _dbContest;

        public CatalogIthemsRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbContest = dbFactory.CreateDbContext();
        }

        public IEnumerable<CatalogItem> FindByShourtCut(string shortCut)
        {
            return _dbContest.CatalogItems.Where(x => x.ShortCut == shortCut).ToList();
        }

        public IEnumerable<CatalogItem> GetAll()
        {
            return _dbContest.CatalogItems.ToList();
        }

        public IEnumerable<CatalogItem> GetCatalogsByCatigoryId(int idType)
        {
            return _dbContest.CatalogItems.Where(x => x.CatalogTypeId == idType).ToList();
        }

        public IEnumerable<CatalogItem> GetCatalogsByCatigoryIdAndName(int idType, string name)
        {
            return _dbContest.CatalogItems.Where(x => x.CatalogTypeId == idType && x.Name.ToUpper().Contains(name.ToUpper())).ToList();
        }

        public IEnumerable<CatalogItem> GetCatalogsByName(string name)
        {
            return _dbContest.CatalogItems.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
        }

        public IEnumerable<CatalogItem> GetItemByBarCode(string barCode)
        {
             return _dbContest.CatalogItems.Where(x => x.Bar_Code == barCode).ToList();
        }
    }
}
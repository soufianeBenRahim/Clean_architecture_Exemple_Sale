using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Clean_Architecture_Soufiane.Domain.Seedwork;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Infrastructure.Repositories
{
    public class SaleRepository
        : ISaleRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly ApplicationDbContext _applicationDB;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _applicationDB;
            }
        }

        public SaleRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            _applicationDB=_dbFactory.CreateDbContext();
        }

        public Sale Add(Sale order)
        {
            return _applicationDB.Sales.Add(order).Entity;
        }

        public async Task<Sale> GetAsync(int orderId)
        {
                var order = await _applicationDB
                            .Sales
                            .FirstOrDefaultAsync(o => o.Id == orderId);
                if (order == null)
                {
                    order = _applicationDB
                                .Sales
                                .Local
                                .FirstOrDefault(o => o.Id == orderId);
                }
                if (order != null)
                {
                    await _applicationDB.Entry(order)
                        .Collection(i => i.SaleItems).LoadAsync();
                    await _applicationDB.Entry(order)
                        .Reference(i => i.SaleStatus).LoadAsync();
                }

                return order;
        
        }

        public void Update(Sale order)
        {
             _applicationDB.Entry(order).State = EntityState.Modified;
        }
    }
}

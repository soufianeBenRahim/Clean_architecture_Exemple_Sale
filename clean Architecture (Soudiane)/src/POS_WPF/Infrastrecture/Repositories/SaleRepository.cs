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

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._dbFactory.CreateDbContext();
            }
        }

        public SaleRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        public Sale Add(Sale order)
        {
            using (ApplicationDbContext db = this._dbFactory.CreateDbContext())
            {
                return db.Sales.Add(order).Entity;
            }
        }

        public async Task<Sale> GetAsync(int orderId)
        {
            using (ApplicationDbContext db = this._dbFactory.CreateDbContext())
            {
                var order = await db
                            .Sales
                            .FirstOrDefaultAsync(o => o.Id == orderId);
                if (order == null)
                {
                    order = db
                                .Sales
                                .Local
                                .FirstOrDefault(o => o.Id == orderId);
                }
                if (order != null)
                {
                    await db.Entry(order)
                        .Collection(i => i.SaleItems).LoadAsync();
                    await db.Entry(order)
                        .Reference(i => i.SaleStatus).LoadAsync();
                }

                return order;
            }

        
        }

        public void Update(Sale order)
        {
            using (ApplicationDbContext db = this._dbFactory.CreateDbContext())
            {
                db.Entry(order).State = EntityState.Modified;
            }
        }
    }
}

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
        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Sale Add(Sale order)
        {
            return _context.Sales.Add(order).Entity;

        }

        public async Task<Sale> GetAsync(int orderId)
        {
            var order = await _context
                                .Sales
                                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                order = _context
                            .Sales
                            .Local
                            .FirstOrDefault(o => o.Id == orderId);
            }
            if (order != null)
            {
                await _context.Entry(order)
                    .Collection(i => i.SaleItems).LoadAsync();
                await _context.Entry(order)
                    .Reference(i => i.SaleStatus).LoadAsync();
            }

            return order;
        }

        public void Update(Sale order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }
    }
}

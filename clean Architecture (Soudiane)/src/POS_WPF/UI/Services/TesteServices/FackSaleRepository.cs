using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Clean_Architecture_Soufiane.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services.TesteServices
{
    public class FackSaleRepository : ISaleRepository
    {
        public FackSaleRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        private IEnumerable<Sale> sales => new List<Sale>()
        {
            new Sale(){
                Created=new DateTime(2022,1,1),
                CreatedBy="Soufiane"}
        };
        public IUnitOfWork UnitOfWork { get; set; }

        public Sale Add(Sale vente)
        {
            sales.Append(vente);
            return vente;
        }

        public Task<Sale> GetAsync(int orderId)
        {
            return Task.FromResult(sales.FirstOrDefault(x => x.Id == orderId));
        }

        public void Update(Sale order)
        {

        }
    }
}

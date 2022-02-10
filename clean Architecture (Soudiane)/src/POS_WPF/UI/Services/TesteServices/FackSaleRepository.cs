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
        private IEnumerable<Sale> sales => new List<Sale>()
        {
            new Sale(){
                Created=new DateTime(2022,1,1),
                CreatedBy="Soufiane"},
            new Sale(){
                Created=new DateTime(2022,2,1),
                CreatedBy="Rahim"},
        };
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Sale Add(Sale vente)
        {
            throw new NotImplementedException();
        }

        public Task<Sale> GetAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public void Update(Sale order)
        {
            throw new NotImplementedException();
        }
    }
}

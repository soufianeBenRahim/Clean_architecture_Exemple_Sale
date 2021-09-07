
using Clean_Architecture_Soufiane.Domain.Seedwork;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface ISaleRepository : IRepository<Sale>
    {
        Sale Add(Sale vente);

        void Update(Sale order);

        Task<Sale> GetAsync(int orderId);
    }
}

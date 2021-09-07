using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Application.Sale.Commands
{
    public class CreateSaleCommand : IRequest<int>
    {

    }

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleCommandHandler(ISaleRepository context)
        {
            _saleRepository = context;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.AggregatesModel.Sales.Sale();

            _saleRepository.Add(entity);

            await _saleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

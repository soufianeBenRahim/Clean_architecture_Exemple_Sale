using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Application.Sale.Commands
{
    public class CreateSaleCommand : IRequest<Guid>
    {

    }

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleCommandHandler(ISaleRepository context)
        {
            _saleRepository = context;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.AggregatesModel.Sales.Sale();

            _saleRepository.Add(entity);

            await _saleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

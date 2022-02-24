using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Application.Sale.Commands
{
    public class ValidateVenteAsCacheCommand : IRequest<bool>
    {
        public Guid idSale { get; set; }
        public decimal amount { get; set; }
    }
    public class ValidateVenteAsCacheCommandHandler : IRequestHandler<ValidateVenteAsCacheCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;

        public ValidateVenteAsCacheCommandHandler(ISaleRepository context)
        {
            _saleRepository = context;
        }

        public async Task<bool> Handle(ValidateVenteAsCacheCommand request, CancellationToken cancellationToken)
        {
            var saleToUpdate = await _saleRepository.GetAsync(request.idSale);
            if (saleToUpdate == null)
            {
                return false;
            }
            if (saleToUpdate.SaleStatus != SaleStatus.AwaitingValidation || saleToUpdate.GetTotal()<=0)
                return false;
            saleToUpdate.SetSaleAsPaidCach(saleToUpdate.GetTotal());
            return await _saleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

}

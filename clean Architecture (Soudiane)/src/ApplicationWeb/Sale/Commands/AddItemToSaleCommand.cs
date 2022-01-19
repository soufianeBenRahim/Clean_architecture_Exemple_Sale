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
    public class AddItemToSaleCommand : IRequest<bool>
    {
        public int idSale { get; set; }
        public int Iditem { get; set; }
        public string itemName { get; set; }
        public decimal Price { get; set; }
        public int numberUnit { get; set; }
    }
    public class AddItemToSaleCommandHandler : IRequestHandler<AddItemToSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;

        public AddItemToSaleCommandHandler(ISaleRepository context)
        {
            _saleRepository = context;
        }

        public async Task<bool> Handle(AddItemToSaleCommand request, CancellationToken cancellationToken)
        {
            var saleToUpdate = await _saleRepository.GetAsync(request.idSale);
            if (saleToUpdate == null)
            {
                return false;
            }

            saleToUpdate.AddSaleItem(request.Iditem, request.itemName, request.Price, 0, "", request.numberUnit);
            return await _saleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}

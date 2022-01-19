using Clean_Architecture_Soufiane.Application.Common.Exceptions;
using Clean_Architecture_Soufiane.Application.Common.Models;
using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
using Clean_Architecture_Soufiane.Domain.Events;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Application.Sale.DomainEventHandlers
{
    class IthemAddToSaleDomainEventHandler : INotificationHandler<DomainEventNotification<IthemAddToSaleDomainEvent>>
    {
        private ApplicationDbContext context;
        public IthemAddToSaleDomainEventHandler (ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task Handle(DomainEventNotification<IthemAddToSaleDomainEvent> notification, CancellationToken cancellationToken)
        {
            var entity = await context.CatalogItems.FindAsync(notification.DomainEvent.idIthem);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CatalogItem), notification.DomainEvent.idIthem);
            }
            entity.RemoveStock(notification.DomainEvent.NbUnit);
            context.CatalogItems.Update(entity);

            await Task.CompletedTask;
        }
    }
}

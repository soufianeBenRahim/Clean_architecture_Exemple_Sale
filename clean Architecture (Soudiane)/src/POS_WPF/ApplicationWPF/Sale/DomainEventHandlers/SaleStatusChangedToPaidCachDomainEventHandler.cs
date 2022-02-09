namespace Clean_Architecture_Soufiane.Application.DomainEventHandlers
{
    using Clean_Architecture_Soufiane.Application.Common.Models;

    using Domain.Events;
    using MediatR;
 
    using Microsoft.Extensions.Logging;

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SaleStatusChangedToPaidCachDomainEventHandler : INotificationHandler<DomainEventNotification<SaleStatusChangedToPaidCachDomainEvent>>
    {
        private readonly ILogger<SaleStatusChangedToPaidCachDomainEvent> _logger;

        public SaleStatusChangedToPaidCachDomainEventHandler(ILogger<SaleStatusChangedToPaidCachDomainEvent> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<SaleStatusChangedToPaidCachDomainEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            _logger.LogInformation("Clean_Architecture_Soufiane Domain Event: {DomainEvent}", domainEvent.GetType().Name);


            // TO DO Add traitement
            return Task.CompletedTask;
        }
    }
}
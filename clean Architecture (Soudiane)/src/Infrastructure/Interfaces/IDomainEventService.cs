using Clean_Architecture_Soufiane.Domain.Seedwork;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}

using Clean_Architecture_Soufiane.Application.Common.Interfaces;
using Clean_Architecture_Soufiane.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using POS.Services;
using System.Linq;
using System.Threading.Tasks;


//[SetUpFixture]
public class Testing
{
  
    private static string _currentUserId;
    private static IDbContextFactory<ApplicationDbContext> dbFactory;
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var isMoke = false;
        var inMemoryDatabase = false;
        var services = new ServiceCollection();
        services.AddLogging();
        ConfigurationService.GetInstance(services, isMoke, inMemoryDatabase);

        // Replace service registration for ICurrentUserService
        // Remove existing registration
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ICurrentUserService));

        services.Remove(currentUserServiceDescriptor);

        // Register testing version
        services.AddTransient(provider =>
            Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));

    }



    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using (var SaleContext = dbFactory.CreateDbContext())
        {
            var mediator = ConfigurationService.getService<ISender>();

            return await mediator.Send(request);
        }
    }

     

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using (var SaleContext = dbFactory.CreateDbContext())
        {
            return await SaleContext.FindAsync<TEntity>(keyValues);
        }
       
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using (var SaleContext = dbFactory.CreateDbContext())
        {

            SaleContext.Add(entity);

            await SaleContext.SaveChangesAsync();
        }

    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {

        using (var SaleContext = dbFactory.CreateDbContext())
        {
            return await SaleContext.Set<TEntity>().CountAsync();
        }
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}

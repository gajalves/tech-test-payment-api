using Microsoft.Extensions.DependencyInjection;

namespace tech_test_payment.infrastructure.Context;

public static class EnsureCreated
{
    public static void EnsureDbCreated(this IServiceProvider service)
    {
        var serviceScopeFactory = service.GetRequiredService<IServiceScopeFactory>();
        using (var serviceScope = serviceScopeFactory.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.infrastructure.Repositories;

namespace tech_test_payment.infrastructure;

public static class InfrastructureDependency
{
    public static IServiceCollection AddInfrasctructureDependencies(this IServiceCollection services)
    {

        AddPersistence(services);
        
        return services;
    }

    private static void AddPersistence(IServiceCollection services)
    {
        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddTransient<IVendedorRepository, VendedorRepository>();
    }
}

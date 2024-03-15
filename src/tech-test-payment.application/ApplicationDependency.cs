using Microsoft.Extensions.DependencyInjection;
using tech_test_payment.application.Interfaces;
using tech_test_payment.application.Services;

namespace tech_test_payment.application;

public static class ApplicationDependency
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        AddService(services);
        
        AddAutoMapper(services);

        return services;
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddSingleton(AutoMapperConfiguration.Create().CreateMapper());
    }

    private static void AddService(IServiceCollection services)
    {
        services.AddTransient<IProdutoService, ProdutoService>();        
        services.AddTransient<IVendedorService, VendedorService>();        
        services.AddTransient<IObterVendaService, ObterVendaService>();        
        services.AddTransient<ICriarVendaService, CriarVendaService>();        
        services.AddTransient<IAtualizarStatusVendaService, AtualizarStatusVendaService>();        
    }
}

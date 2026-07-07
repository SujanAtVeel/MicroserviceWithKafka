using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Facades;
using ProductService.Domain.Ports;

namespace ProductService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<ProductFacade>();
        
        return services;
    }
}
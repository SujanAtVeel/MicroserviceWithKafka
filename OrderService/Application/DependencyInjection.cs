using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Facades;
using OrderService.Domain.Ports;

namespace OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderFacade, OrderFacade>();
        
        return services;
    }
}
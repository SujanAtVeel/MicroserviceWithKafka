using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Ports;
using ProductService.Infrastructure.Messaging;
using ProductService.Infrastructure.Persistence;
using ProductService.Infrastructure.Persistence.Repositories;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProcessedEventStore, ProcessedEventStore>();
        services.AddHostedService<OrderCreatedConsumer>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductDbContext>());

        services.Configure<KafkaSettings>(options =>
       {
           options.BootstrapServers = configuration.GetConnectionString("kafka") ?? "localhost:9092";
       });

        // DbContext
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        return services;
    }
}
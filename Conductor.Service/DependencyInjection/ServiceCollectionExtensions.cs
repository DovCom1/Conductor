using Conductor.Models;
using Conductor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Conductor.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = false;
            });
        
        services.Configure<ServiceRegistry>(configuration.GetSection("ServiceRegistry"));
        
        var serviceRegistry = configuration.GetSection("ServiceRegistry").Get<ServiceRegistry>();
        if (serviceRegistry != null)
        {
            RegisterHttpClients(services, serviceRegistry);
        }
        
        RegisterApplicationServices(services);

        services.AddSwaggerGen();

        return services;
    }

    private static void RegisterHttpClients(IServiceCollection services, ServiceRegistry serviceRegistry)
    {
        foreach (var serviceConfig in serviceRegistry.GetAllServices())
        {
            services.AddHttpClient(serviceConfig.Name, client =>
            {
                client.BaseAddress = new Uri(serviceConfig.BaseUrl);
            });
        }
    }

    private static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddSingleton<IRouteService, RouteService>();
    }
}
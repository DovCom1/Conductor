using Conductor.Managers;
using Conductor.Model.Interfaces.Managers;
using Conductor.Model.Interfaces.Services;
using Conductor.Model.ServiceRegistry;
using Conductor.Models.Interfaces.Managers;
using Conductor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        
        services.Configure<ServiceRegistry>(configuration.GetSection("ServiceRegistry"))
            .PostConfigure<ServiceRegistry>(options =>
            {
                foreach (var service in options.GetAllServices())
                {
                    var configuredUrl = configuration[$"{service.Name}:BaseUrl"];
                    service.BaseUrl = !string.IsNullOrEmpty(configuredUrl) ? configuredUrl : service.BaseUrl;
                }
            });

        RegisterHttpClients(services);

        RegisterApplicationServices(services);

        services.AddSwaggerGen();

        return services;
    }

    private static void RegisterHttpClients(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var serviceRegistry = serviceProvider.GetRequiredService<IOptions<ServiceRegistry>>().Value;

        foreach (var serviceConfig in serviceRegistry.GetAllServices())
        {
            services.AddHttpClient(serviceConfig.Name, (sp, client) =>
            {
                client.BaseAddress = new Uri(serviceConfig.BaseUrl);
            });
        }
    }

    private static void RegisterApplicationServices(IServiceCollection services)
    {
        services
            .AddScoped<IRouteService, RouteService>()
            .AddScoped<IUsersManager, UsersManager>()
            .AddScoped<IChatsManager, ChatsManager>()
            .AddScoped<ISettingsManager, SettingsManager>();
    }
}
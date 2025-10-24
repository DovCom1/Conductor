using Conductor.Models;

namespace Conductor.Model.ServiceRegistry;

public class ServiceRegistry
{
    public required ServiceConfig UserService { get; set; }
    public required ServiceConfig ChatService { get; set; }
    public required ServiceConfig SettingsService { get; set; }
    public required ServiceConfig SearchService { get; set; }
    
    public ServiceConfig GetServiceConfig(string serviceName)
    {
        return serviceName.ToLower() switch
        {
            Constants.Constants.UserServiceName => UserService,
            Constants.Constants.ChatServiceName => ChatService,
            Constants.Constants.SettingsServiceName => SettingsService,
            Constants.Constants.SearchServiceName => SearchService,
            _ => throw new ArgumentException($"Unknown service: {serviceName}")
        };
    }
    
    public List<ServiceConfig> GetAllServices()
    {
        return
        [
            UserService,
            ChatService,
            SettingsService,
            SearchService
        ];
    }
}
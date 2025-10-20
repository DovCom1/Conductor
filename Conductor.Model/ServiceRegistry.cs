namespace Conductor.Models;

public class ServiceRegistry
{
    public ServiceConfig UserService { get; set; }
    public ServiceConfig ChatService { get; set; }
    public ServiceConfig SettingsService { get; set; }
    public ServiceConfig SearchService { get; set; }
    
    public ServiceConfig GetServiceConfig(string serviceName)
    {
        return serviceName.ToLower() switch
        {
            Constants.UserServiceName => UserService,
            Constants.ChatServiceName => ChatService,
            Constants.SettingsServiceName => SettingsService,
            Constants.SearchServiceName => SearchService,
            _ => throw new ArgumentException($"Unknown service: {serviceName}")
        };
    }
    
    public List<ServiceConfig> GetAllServices()
    {
        return new List<ServiceConfig>
        {
            UserService,
            ChatService,
            SettingsService,
            SearchService
        };
    }
}
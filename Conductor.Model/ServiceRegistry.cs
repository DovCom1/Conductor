namespace Conductor.Models;

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
            Constants.UserServiceName => UserService,
            Constants.ChatServiceName => ChatService,
            Constants.SettingsServiceName => SettingsService,
            Constants.SearchServiceName => SearchService,
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
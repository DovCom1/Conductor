using System.Text;
using System.Text.Json;
using Conductor.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Conductor.Services;

public class RouteService : IRouteService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ServiceRegistry _serviceRegistry;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<RouteService> _logger;

    public RouteService(IHttpClientFactory httpClientFactory, IOptions<ServiceRegistry> serviceRegistry, ILogger<RouteService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _serviceRegistry = serviceRegistry.Value;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(TRequest request, string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);
            
            HttpContent content;
            if (request == null)
            {
                content = new StringContent("null", Encoding.UTF8, "application/json");
            }
            else
            {
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            
            _logger.LogInformation("Sending POST request to {Url}", endpoint);
            var response = await client.PostAsync($"{serviceConfig.BaseUrl}{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                var data = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
                return Result<TResponse>.Success(data, (int)response.StatusCode);
            }
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result<TResponse>.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during POST request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result<TResponse>.Failure($"Conductor error: {ex.Message}", 500);
        }
    }

    public async Task<Result<TResponse>> PostAsync<TResponse>(string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);
            
            var content = new StringContent("null", Encoding.UTF8, "application/json");
            
            _logger.LogInformation("Sending POST request to {Url}", endpoint);
            var response = await client.PostAsync($"{serviceConfig.BaseUrl}{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                var data = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
                return Result<TResponse>.Success(data, (int)response.StatusCode);
            }
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result<TResponse>.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during POST request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result<TResponse>.Failure($"Conductor error: {ex.Message}", 500);
        }
    }
    
    public async Task<Result> PostAsync<TRequest>(TRequest request, string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);
            
            HttpContent content;
            if (request == null)
            {
                content = new StringContent("null", Encoding.UTF8, "application/json");
            }
            else
            {
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            _logger.LogInformation("Sending POST request to {Url}", endpoint);
            var response = await client.PostAsync($"{serviceConfig.BaseUrl}{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                return Result.Success((int)response.StatusCode);
            }
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during POST request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result.Failure($"Conductor error: {ex.Message}", 500);
        }
    }

    public async Task<Result<TResponse>> GetAsync<TResponse>(string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);

            _logger.LogInformation("Sending Get request to {Url}", endpoint);
            var response = await client.GetAsync($"{serviceConfig.BaseUrl}{endpoint}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                var data = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
                return Result<TResponse>.Success(data, (int)response.StatusCode);
            }
            
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result<TResponse>.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Get request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result<TResponse>.Failure($"Conductor error: {ex.Message}", 500);
        }
    }

    public async Task<Result> DeleteAsync(string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);

            _logger.LogInformation("Sending Delete request to {Url}", endpoint);
            var response = await client.DeleteAsync($"{serviceConfig.BaseUrl}{endpoint}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                return Result.Success((int)response.StatusCode);
            }
            
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Delete request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result.Failure($"Conductor error: {ex.Message}", 500);
        }
    }

    public async Task<Result<TResponse>> PatchAsync<TRequest, TResponse>(TRequest request, string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);

            HttpContent content;
            if (request == null)
            {
                content = new StringContent("null", Encoding.UTF8, "application/json");
            }
            else
            {
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            
            _logger.LogInformation("Sending Patch request to {Url}", endpoint);
            var response = await client.PatchAsync($"{serviceConfig.BaseUrl}{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                var data = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
                return Result<TResponse>.Success(data, (int)response.StatusCode);
            }
            
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result<TResponse>.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Patch request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result<TResponse>.Failure($"Conductor error: {ex.Message}", 500);
        }
    }

    public async Task<Result<TResponse>> PatchAsync<TResponse>(string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);

            var content = new StringContent("null", Encoding.UTF8, "application/json");
            
            _logger.LogInformation("Sending Patch request to {Url}", endpoint);
            var response = await client.PatchAsync($"{serviceConfig.BaseUrl}{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                var data = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
                return Result<TResponse>.Success(data, (int)response.StatusCode);
            }
            
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result<TResponse>.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Patch request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result<TResponse>.Failure($"Conductor error: {ex.Message}", 500);
        }
    }

    public async Task<Result> PatchAsync(string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);

            var content = new StringContent("null", Encoding.UTF8, "application/json");

            _logger.LogInformation("Sending Patch request to {Url}", endpoint);
            var response = await client.PatchAsync($"{serviceConfig.BaseUrl}{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                return Result.Success((int)response.StatusCode);
            }

            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}",
                (int)response.StatusCode, responseContent);
            return Result.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Patch request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result.Failure($"Conductor error: {ex.Message}", 500);
        }
    }

    public async Task<Result> PutAsync<TRequest>(TRequest request, string serviceName, string endpoint)
    {
        try
        {
            var serviceConfig = _serviceRegistry.GetServiceConfig(serviceName);
            var client = _httpClientFactory.CreateClient(serviceName);

            HttpContent content;
            
            if (request == null)
            {
                content = new StringContent("null", Encoding.UTF8, "application/json");
            }
            else
            {
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            
            _logger.LogInformation("Sending Put request to {Url}", endpoint);
            var response = await client.PutAsync($"{serviceConfig.BaseUrl}{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successful (Status: {StatusCode})", (int)response.StatusCode);
                return Result.Success((int)response.StatusCode);
            }
            
            _logger.LogWarning("Request failed with status {StatusCode}: {ResponseContent}", 
                (int)response.StatusCode, responseContent);
            return Result.Failure(responseContent, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Put request to {ServiceName}{Endpoint}", serviceName, endpoint);
            return Result.Failure($"Conductor error: {ex.Message}", 500);
        }
    }
}
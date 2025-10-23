using Conductor.Models;

namespace Conductor.Services;

public interface IRouteService
{
    Task<Result<TResponse>> PostAsync<TRequest, TResponse>(TRequest request, string serviceName, string endpoint);
    Task<Result<TResponse>> PostAsync<TResponse>(string serviceName, string endpoint);
    Task<Result> PostAsync<TRequest>(TRequest request, string serviceName, string endpoint);
    Task<Result<TResponse>> GetAsync<TResponse>(string serviceName, string endpoint);
    Task<Result> DeleteAsync(string serviceName, string endpoint);
    Task<Result<TResponse>> PatchAsync<TRequest, TResponse>(TRequest request, string serviceName, string endpoint);
    Task<Result<TResponse>> PatchAsync<TResponse>(string serviceName, string endpoint);
    Task<Result> PatchAsync(string serviceName, string endpoint);
    Task<Result> PutAsync<TRequest>(TRequest request, string serviceName, string endpoint);
}
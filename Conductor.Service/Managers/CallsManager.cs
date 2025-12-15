using Conductor.Dto.Chat;
using Conductor.Model.Constants;
using Conductor.Model.Dto.Calls;
using Conductor.Model.Dto.Calls.Rooms;
using Conductor.Model.Interfaces.Managers;
using Conductor.Model.Interfaces.Services;
using Conductor.Models;
using Microsoft.Extensions.Logging;

namespace Conductor.Managers
{
    public class CallsManager : ICallsManager
    {
        private readonly IRouteService _routeService;
        private readonly ILogger<ChatsManager> _logger;

        public CallsManager(IRouteService routeService, ILogger<ChatsManager> logger)
        {
            _routeService = routeService;
            _logger = logger;
        }

        public async Task<Result> SendSignalMessageAsync(SignalingMessageDto request)
        {
            return await _routeService.PostAsync<SignalingMessageDto>(
            request,
            Constants.CallsServiceName,
            "/api/signal");
        }

        public async Task<Result<RoomInfoDto>> SendCreateRoomRequestAsync(CreateRoomRequest request)
        {
            return await _routeService.PostAsync<CreateRoomRequest, RoomInfoDto>(
            request,
            Constants.CallsServiceName,
            "/api");
        }

        public async Task<Result<RoomInfoDto>> GetRoomByRoomIdAsync(Guid roomId)
        {
            return await _routeService.GetAsync<RoomInfoDto>(
                Constants.CallsServiceName,
                $"/api/{roomId}");
        }

        public async Task<Result<List<RoomInfoDto>>> GetRoomsByUserIdAsync(Guid userId)
        {
            return await _routeService.GetAsync<List<RoomInfoDto>>(
                Constants.CallsServiceName,
                $"/api/user/{userId}");
        }

        public async Task<Result> DeleteRoomAsync(Guid roomId)
        {
            return await _routeService.DeleteAsync(
                Constants.CallsServiceName,
                $"/api/rooms/{roomId}");
        }
    }
}

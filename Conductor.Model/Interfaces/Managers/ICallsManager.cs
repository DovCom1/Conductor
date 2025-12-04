using Conductor.Model.Dto.Calls;
using Conductor.Model.Dto.Calls.Rooms;
using Conductor.Models;

namespace Conductor.Model.Interfaces.Managers
{
    public interface ICallsManager
    {
        Task<Result> SendSignalMessageAsync(SignalingMessageDto request);
        Task<Result<RoomInfoDto>> SendCreateRoomRequestAsync(CreateRoomRequest request);
        Task<Result<RoomInfoDto>> GetRoomByRoomIdAsync(Guid roomId);
        Task<Result<List<RoomInfoDto>>> GetRoomsByUserIdAsync(Guid userId);
        Task<Result> DeleteRoomAsync(Guid roomId);
    }
}

using Conductor.Model.Dto.Calls;
using Conductor.Model.Dto.Calls.Rooms;
using Conductor.Model.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ConductorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallsController : ControllerBase
    {
        private readonly ICallsManager _callsManager;

        public CallsController(ICallsManager callsManager)
        {
            _callsManager = callsManager;
        }

        [HttpPost("signal")]
        public async Task<IActionResult> SendSignalMessageAsync([FromBody] SignalingMessageDto request)
        {
            var result = await _callsManager.SendSignalMessageAsync(request);
            return result.IsSuccess ? Ok() : StatusCode(result.StatusCode, result.Error);
        }

        [HttpPost("rooms")]
        public async Task<ActionResult<RoomInfoDto>> SendCreateRoomRequestAsync([FromBody] CreateRoomRequest request)
        {
            var result = await _callsManager.SendCreateRoomRequestAsync(request);
            return result.IsSuccess ? Ok(result.Data) : StatusCode(result.StatusCode, result.Error);
        }

        [HttpGet("rooms/{roomId}")]
        public async Task<ActionResult<RoomInfoDto>> GetRoomByRoomIdAsync(Guid roomId)
        {
            var result = await _callsManager.GetRoomByRoomIdAsync(roomId);
            return result.IsSuccess ? Ok(result.Data) : StatusCode(result.StatusCode, result.Error);
        }

        [HttpGet("rooms/user/{userId}")]
        public async Task<ActionResult<List<RoomInfoDto>>> GetRoomsByUserIdAsync(Guid userId)
        {
            var result = await _callsManager.GetRoomsByUserIdAsync(userId);
            return result.IsSuccess ? Ok(result.Data) : StatusCode(result.StatusCode, result.Error);
        }

        [HttpDelete("rooms/{roomId}")]
        public async Task<IActionResult> DeleteRoomAsync(Guid roomId)
        {
            var result = await _callsManager.DeleteRoomAsync(roomId); 
            return result.IsSuccess ? Ok() : StatusCode(result.StatusCode, result.Error);
        }
    }

}

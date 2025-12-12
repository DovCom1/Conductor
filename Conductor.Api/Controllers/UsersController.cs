using Conductor.Dto.Search;
using Conductor.Dto.Users;
using Conductor.Models.Interfaces.Managers;
using Conductor.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace ConductorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUsersManager _usersManager;

    public UsersController(IUsersManager usersManager)
    {
        _usersManager = usersManager;
    }
    
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfoAsync(Guid userId)
    {
        var result = await _usersManager.GetUserInfoAsync(userId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpGet("{userId}/main")]
    public async Task<ActionResult<UserInfoMainDto>> GetUserInfoMainAsync(Guid userId)
    {
        var result = await _usersManager.GetUserInfoMainAsync(userId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers(
    [FromQuery] string uid = null,
    [FromQuery] string nickname = null,
    [FromQuery] int offset = 0,
    [FromQuery] int limit = 20)
    {
        if (!string.IsNullOrEmpty(uid) && string.IsNullOrEmpty(nickname))
        {
            var result = await _usersManager.GetUserByUidAsync(uid);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }
        else if (!string.IsNullOrEmpty(nickname) && string.IsNullOrEmpty(uid))
        {
            var result = await _usersManager.GetUsersByNicknameAsync(nickname, offset, limit);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }
        else
        {
            return BadRequest("Specify either 'uid' OR 'nickname' parameter");
        }
    }
    
    [HttpPost("{userId}/friends/{friendId}")]
    public async Task<ActionResult<FriendResponseDto>> SendFriendRequestAsync(Guid userId, Guid friendId)
    {
        var result = await _usersManager.SendFriendRequestAsync(userId, friendId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpPatch("{userId}/friends/{friendId}/accept")]
    public async Task<ActionResult<FriendResponseDto>> AcceptFriendRequestAsync(Guid userId, Guid friendId)
    {
        var result = await _usersManager.AcceptFriendRequestAsync(userId, friendId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpPatch("{userId}/friends/{friendId}/reject")]
    public async Task<IActionResult> RejectFriendRequestAsync(Guid userId, Guid friendId)
    {
        var result = await _usersManager.RejectFriendRequestAsync(userId, friendId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok();
    }
    
    [HttpDelete("{userId}/friends/{friendId}")]
    public async Task<IActionResult> DeleteFriendRequestAsync(Guid userId, Guid friendId)
    {
        var result = await _usersManager.DeleteFriendRequestAsync(userId, friendId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok();
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserInfoDto>> RegisterUserAsync([FromBody] RegisterUserData registerUserData)
    {
        var result = await _usersManager.RegisterUserAsync(registerUserData);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpPatch("{userId}")]
    public async Task<ActionResult<UserInfoDto>> UpdateUserAsync(Guid userId, [FromBody] UpdateUserInfoDto userInfo)
    {
        var result = await _usersManager.UpdateUserAsync(userId, userInfo);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        var result = await _usersManager.DeleteUserAsync(userId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<UserListDto<UserInfoDto>>> GetUsersAsync([FromQuery] int offset, [FromQuery] int limit)
    {
        var result = await _usersManager.GetUsersAsync(offset, limit);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpGet("main")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetUsersMainAsync([FromQuery] int offset, [FromQuery] int limit)
    {
        var result = await _usersManager.GetUsersMainAsync(offset, limit);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpPost("{userId}/enemies/{enemyId}")]
    public async Task<ActionResult<EnemyResponseDto>> AddUserToEnemiesAsync(Guid userId, Guid enemyId)
    {
        var result = await _usersManager.AddUserToEnemiesAsync(userId, enemyId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpDelete("{userId}/enemies/{enemyId}")]
    public async Task<IActionResult> DeleteUserFromEnemiesAsync(Guid userId, Guid enemyId)
    {
        var result = await _usersManager.DeleteUserFromEnemiesAsync(userId, enemyId);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok();
    }
    
    [HttpGet("{userId}/friends")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetFriendsAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var result = await _usersManager.GetFriendsAsync(userId, offset, limit);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpGet("{userId}/enemies")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetEnemiesAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var result = await _usersManager.GetEnemiesAsync(userId, offset, limit);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpGet("{userId}/friends/requests/incoming")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetIncomingRequestsAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var result = await _usersManager.GetIncomingRequestsAsync(userId, offset, limit);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
    
    [HttpGet("{userId}/friends/requests/outgoing")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetOutgoingRequestsAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var result = await _usersManager.GetOutgoingRequestsAsync(userId, offset, limit);
        return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
    }
}
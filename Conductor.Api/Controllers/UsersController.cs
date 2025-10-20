using Conductor.Dto.Search;
using Conductor.Dto.Users;
using Conductor.Models;
using Conductor.Models.Users;
using Conductor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Conductor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IRouteService _routeService;

    public UsersController(IRouteService routeService)
    {
        _routeService = routeService;
    }
    
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfoAsync(Guid userId)
    {
        var userInfo = await _routeService.GetAsync<UserInfoDto>(Constants.UserServiceName, $"/api/users/{userId}");
        return !userInfo.IsSuccess ? StatusCode(userInfo.StatusCode, $"An error occurred: {userInfo.Error}") : Ok(userInfo.Data);
    }
    
    [HttpGet("{userId}/main")]
    public async Task<ActionResult<UserInfoMainDto>> GetUserInfoMainAsync(Guid userId)
    {
        var userInfo = await _routeService.GetAsync<UserInfoMainDto>(Constants.UserServiceName, $"/api/users/{userId}/main");
        return !userInfo.IsSuccess ? StatusCode(userInfo.StatusCode, $"An error occurred: {userInfo.Error}") : Ok(userInfo.Data);
    }

    [HttpGet("search")]
    public async Task<ActionResult<UserInfoMainDto>> GetUserByUidAsync([FromQuery] string uid)
    {
        var userInfo = await _routeService.GetAsync<UserInfoMainDto>(Constants.SearchServiceName, $"/api/users/search?uid={uid}");
        return !userInfo.IsSuccess ? StatusCode(userInfo.StatusCode, $"An error occurred: {userInfo.Error}") : Ok(userInfo.Data);
    }
    
    [HttpGet("search/{nickname}")]
    public async Task<ActionResult<SearchNicknameResponseDto>> GetUsersByNicknameAsync(
        string nickname,
        [FromQuery] int offset,
        [FromQuery] int limit)
    {
        var users = await _routeService.GetAsync<SearchNicknameResponseDto>(Constants.SearchServiceName,
            $"/api/users/search?nickname={nickname}&offset={offset}&limit={limit}");
        return !users.IsSuccess ? StatusCode(users.StatusCode, $"An error occurred: {users.Error}") : Ok(users.Data);
    }
    
    [HttpPost("{userId}/friends/{friendId}")]
    public async Task<ActionResult<FriendResponseDto>> SendFriendRequestAsync(Guid userId, Guid friendId)
    {
        var sendRequest = await _routeService.PostAsync<FriendResponseDto>(Constants.UserServiceName,
            $"/api/users/{userId}/friends/{friendId}");
        return !sendRequest.IsSuccess ? StatusCode(sendRequest.StatusCode, $"An error occurred: {sendRequest.Error}") : Ok(sendRequest.Data);
    }
    
    [HttpPatch("{userId}/friends/{friendId}/accept")]
    public async Task<ActionResult<FriendResponseDto>> AcceptFriendRequestAsync(Guid userId, Guid friendId)
    {
        var acceptRequest = await _routeService.PatchAsync<FriendResponseDto>(Constants.UserServiceName,
            $"/api/users/{userId}/friends/{friendId}/accept");
        return !acceptRequest.IsSuccess ? StatusCode(acceptRequest.StatusCode, $"An error occurred: {acceptRequest.Error}") : Ok(acceptRequest.Data);
    }
    
    [HttpPatch("{userId}/friends/{friendId}/reject")]
    public async Task<ActionResult<FriendResponseDto>> RejectFriendRequestAsync(Guid userId, Guid friendId)
    {
        var rejectRequest = await _routeService.PatchAsync<FriendResponseDto>(Constants.UserServiceName,
            $"/api/users/{userId}/friends/{friendId}/reject");
        return !rejectRequest.IsSuccess ? StatusCode(rejectRequest.StatusCode, $"An error occurred: {rejectRequest.Error}") : Ok();
    }
    
    [HttpDelete("{userId}/friends/{friendId}")]
    public async Task<IActionResult> DeleteFriendRequestAsync(Guid userId, Guid friendId)
    {
        var deleteRequest = await _routeService.DeleteAsync(Constants.UserServiceName, $"/api/users/{userId}/friends/{friendId}");
        return !deleteRequest.IsSuccess ? StatusCode(deleteRequest.StatusCode, $"An error occurred: {deleteRequest.Error}") : Ok();
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserInfoDto>> RegisterUserAsync([FromBody] RegisterUserData registerUserData)
    {
        var registerUser = await _routeService.PostAsync<RegisterUserData, UserInfoDto>(registerUserData, Constants.UserServiceName, $"/api/users/register");
        return !registerUser.IsSuccess ? StatusCode(registerUser.StatusCode, $"An error occurred: {registerUser.Error}") : Ok();
    }
    
    [HttpPatch("{userId}")]
    public async Task<ActionResult<UserInfoDto>> UpdateUserAsync(Guid userId, [FromBody] UpdateUserInfoDto userInfo)
    {
        var updateUser = await _routeService.PatchAsync<UpdateUserInfoDto, UserInfoDto>(userInfo, Constants.UserServiceName, $"/api/users/{userId}");
        return !updateUser.IsSuccess ? StatusCode(updateUser.StatusCode, $"An error occurred: {updateUser.Error}") : Ok();
    }
    
    [HttpDelete("{userId}")]
    public async Task<ActionResult<UserInfoDto>> DeleteUserAsync(Guid userId)
    {
        var deleteUser = await _routeService.DeleteAsync(Constants.UserServiceName, $"/api/users/{userId}");
        return !deleteUser.IsSuccess ? StatusCode(deleteUser.StatusCode, $"An error occurred: {deleteUser.Error}") : Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<UserListDto<UserInfoDto>>> GetUsersAsync([FromQuery] int offset, [FromQuery] int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoDto>>(Constants.UserServiceName, $"/api/users?offset={offset}&limit={limit}");
        return !users.IsSuccess ? StatusCode(users.StatusCode, $"An error occurred: {users.Error}") : Ok();
    }
    
    [HttpGet("main")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetUsersMainAsync([FromQuery] int offset, [FromQuery] int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoMainDto>>(Constants.UserServiceName, $"/api/users/main?offset={offset}&limit={limit}");
        return !users.IsSuccess ? StatusCode(users.StatusCode, $"An error occurred: {users.Error}") : Ok();
    }
    
    [HttpPost("{userId}/enemies/{enemyId}")]
    public async Task<ActionResult<UserInfoDto>> AddUserToEnemiesAsync(Guid userId, Guid enemyId)
    {
        var addEnemy = await _routeService.PostAsync<UserInfoDto>(Constants.UserServiceName, $"/api/users/{userId}/enemies/{enemyId}");
        return !addEnemy.IsSuccess ? StatusCode(addEnemy.StatusCode, $"An error occurred: {addEnemy.Error}") : Ok();
    }
    
    [HttpDelete("{userId}/enemies/{enemyId}")]
    public async Task<IActionResult> DeleteUserFromEnemiesAsync(Guid userId, Guid enemyId)
    {
        var deleteFromEnemies = await _routeService.DeleteAsync(Constants.UserServiceName, $"/api/users/{userId}/enemies/{enemyId}");
        return !deleteFromEnemies.IsSuccess ? StatusCode(deleteFromEnemies.StatusCode, $"An error occurred: {deleteFromEnemies.Error}") : Ok();
    }
    
    [HttpGet("{userId}/friends")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetFriendsAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var users = await _routeService.GetAsync<UserInfoMainDto>(Constants.UserServiceName, $"/api/users/{userId}/friends?offset={offset}&limit={limit}");
        return !users.IsSuccess ? StatusCode(users.StatusCode, $"An error occurred: {users.Error}") : Ok();
    }
    
    [HttpGet("{userId}/enemies")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetEnemiesAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var users = await _routeService.GetAsync<UserInfoMainDto>(Constants.UserServiceName, $"/api/users/{userId}/enemies?offset={offset}&limit={limit}");
        return !users.IsSuccess ? StatusCode(users.StatusCode, $"An error occurred: {users.Error}") : Ok();
    }
    
    [HttpGet("{userId}/friends/requests/incoming")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetIncomingRequestsAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var users = await _routeService.GetAsync<UserInfoMainDto>(Constants.UserServiceName, $"/api/users/{userId}/friends/requests/incoming?offset={offset}&limit={limit}");
        return !users.IsSuccess ? StatusCode(users.StatusCode, $"An error occurred: {users.Error}") : Ok();
    }
    
    [HttpGet("{userId}/friends/requests/outgoing")]
    public async Task<ActionResult<UserListDto<UserInfoMainDto>>> GetOutgoingRequestsAsync(Guid userId, [FromQuery] int offset, [FromQuery] int limit)
    {
        var users = await _routeService.GetAsync<UserInfoMainDto>(Constants.UserServiceName, $"/api/users/{userId}/friends/requests/outgoing?offset={offset}&limit={limit}");
        return !users.IsSuccess ? StatusCode(users.StatusCode, $"An error occurred: {users.Error}") : Ok();
    }
}
using Conductor.Dto.Search;
using Conductor.Dto.Users;
using Conductor.Model.Constants;
using Conductor.Model.Interfaces.Services;
using Conductor.Models;
using Conductor.Models.Interfaces.Managers;
using Conductor.Models.Users;

namespace Conductor.Managers;

public class UsersManager : IUsersManager
{
    private readonly IRouteService _routeService;

    public UsersManager(IRouteService routeService)
    {
        _routeService = routeService;
    }

    public async Task<Result<UserInfoDto>> GetUserInfoAsync(Guid userId)
    {
        var userInfo = await _routeService.GetAsync<UserInfoDto>(Constants.UserServiceName, $"/api/users/{userId}");
        return userInfo;
    }

    public async Task<Result<UserInfoMainDto>> GetUserInfoMainAsync(Guid userId)
    {
        var userInfo = await _routeService.GetAsync<UserInfoMainDto>(Constants.UserServiceName, $"/api/users/{userId}/main");
        return userInfo;
    }

    public async Task<Result<UserInfoMainDto>> GetUserByUidAsync(string uid)
    {
        var userInfo = await _routeService.GetAsync<UserInfoMainDto>(Constants.SearchServiceName, $"/api/users/search?uid={uid}");
        return userInfo;
    }

    public async Task<Result<SearchNicknameResponseDto>> GetUsersByNicknameAsync(string nickname, int offset, int limit)
    {
        var users = await _routeService.GetAsync<SearchNicknameResponseDto>(Constants.SearchServiceName,
            $"/api/users/search?nickname={nickname}&offset={offset}&limit={limit}");
        return users;
    }

    public async Task<Result<FriendResponseDto>> SendFriendRequestAsync(Guid userId, Guid friendId)
    {
        var sendRequest = await _routeService.PostAsync<FriendResponseDto>(Constants.UserServiceName,
            $"/api/users/{userId}/friends/{friendId}");
        return sendRequest;
    }

    public async Task<Result<FriendResponseDto>> AcceptFriendRequestAsync(Guid userId, Guid friendId)
    {
        var acceptRequest = await _routeService.PatchAsync<FriendResponseDto>(Constants.UserServiceName,
            $"/api/users/{userId}/friends/{friendId}/accept");
        return acceptRequest;
    }

    public async Task<Result> RejectFriendRequestAsync(Guid userId, Guid friendId)
    {
        var rejectRequest = await _routeService.PatchAsync(Constants.UserServiceName,
            $"/api/users/{userId}/friends/{friendId}/reject");
        return rejectRequest;
    }

    public async Task<Result> DeleteFriendRequestAsync(Guid userId, Guid friendId)
    {
        var deleteRequest = await _routeService.DeleteAsync(Constants.UserServiceName, $"/api/users/{userId}/friends/{friendId}");
        return deleteRequest;
    }

    public async Task<Result<UserInfoDto>> RegisterUserAsync(RegisterUserData registerUserData)
    {
        var registerUser = await _routeService.PostAsync<RegisterUserData, UserInfoDto>(registerUserData, Constants.UserServiceName, $"/api/users/register");
        return registerUser;
    }

    public async Task<Result<UserInfoDto>> UpdateUserAsync(Guid userId, UpdateUserInfoDto userInfo)
    {
        var updateUser = await _routeService.PatchAsync<UpdateUserInfoDto, UserInfoDto>(userInfo, Constants.UserServiceName, $"/api/users/{userId}");
        return updateUser;
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var deleteUser = await _routeService.DeleteAsync(Constants.UserServiceName, $"/api/users/{userId}");
        return deleteUser;
    }

    public async Task<Result<UserListDto<UserInfoDto>>> GetUsersAsync(int offset, int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoDto>>(Constants.UserServiceName, $"/api/users?offset={offset}&limit={limit}");
        return users;
    }

    public async Task<Result<UserListDto<UserInfoMainDto>>> GetUsersMainAsync(int offset, int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoMainDto>>(Constants.UserServiceName, $"/api/users/main?offset={offset}&limit={limit}");
        return users;
    }

    public async Task<Result<EnemyResponseDto>> AddUserToEnemiesAsync(Guid userId, Guid enemyId)
    {
        var addEnemy = await _routeService.PostAsync<EnemyResponseDto>(Constants.UserServiceName, $"/api/users/{userId}/enemies/{enemyId}");
        return addEnemy;
    }

    public async Task<Result> DeleteUserFromEnemiesAsync(Guid userId, Guid enemyId)
    {
        var deleteFromEnemies = await _routeService.DeleteAsync(Constants.UserServiceName, $"/api/users/{userId}/enemies/{enemyId}");
        return deleteFromEnemies;
    }

    public async Task<Result<UserListDto<UserInfoMainDto>>> GetFriendsAsync(Guid userId, int offset, int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoMainDto>>(Constants.UserServiceName, $"/api/users/{userId}/friends?offset={offset}&limit={limit}");
        return users;
    }

    public async Task<Result<UserListDto<UserInfoMainDto>>> GetEnemiesAsync(Guid userId, int offset, int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoMainDto>>(Constants.UserServiceName, $"/api/users/{userId}/enemies?offset={offset}&limit={limit}");
        return users;
    }

    public async Task<Result<UserListDto<UserInfoMainDto>>> GetIncomingRequestsAsync(Guid userId, int offset, int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoMainDto>>(Constants.UserServiceName, $"/api/users/{userId}/friends/requests/incoming?offset={offset}&limit={limit}");
        return users;
    }

    public async Task<Result<UserListDto<UserInfoMainDto>>> GetOutgoingRequestsAsync(Guid userId, int offset, int limit)
    {
        var users = await _routeService.GetAsync<UserListDto<UserInfoMainDto>>(Constants.UserServiceName, $"/api/users/{userId}/friends/requests/outgoing?offset={offset}&limit={limit}");
        return users;
    }
}
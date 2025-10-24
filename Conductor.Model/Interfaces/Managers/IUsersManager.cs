using Conductor.Dto.Search;
using Conductor.Dto.Users;
using Conductor.Models.Users;

namespace Conductor.Models.Interfaces.Managers;

public interface IUsersManager
{
    Task<Result<UserInfoDto>> GetUserInfoAsync(Guid userId);
    Task<Result<UserInfoMainDto>> GetUserInfoMainAsync(Guid userId);
    Task<Result<UserInfoMainDto>> GetUserByUidAsync(string uid);
    Task<Result<SearchNicknameResponseDto>> GetUsersByNicknameAsync(string nickname, int offset, int limit);
    Task<Result<FriendResponseDto>> SendFriendRequestAsync(Guid userId, Guid friendId);
    Task<Result<FriendResponseDto>> AcceptFriendRequestAsync(Guid userId, Guid friendId);
    Task<Result> RejectFriendRequestAsync(Guid userId, Guid friendId);
    Task<Result> DeleteFriendRequestAsync(Guid userId, Guid friendId);
    Task<Result<UserInfoDto>> RegisterUserAsync(RegisterUserData registerUserData);
    Task<Result<UserInfoDto>> UpdateUserAsync(Guid userId, UpdateUserInfoDto userInfo);
    Task<Result> DeleteUserAsync(Guid userId);
    Task<Result<UserListDto<UserInfoDto>>> GetUsersAsync(int offset, int limit);
    Task<Result<UserListDto<UserInfoMainDto>>> GetUsersMainAsync(int offset, int limit);
    Task<Result<EnemyResponseDto>> AddUserToEnemiesAsync(Guid userId, Guid enemyId);
    Task<Result> DeleteUserFromEnemiesAsync(Guid userId, Guid enemyId);
    Task<Result<UserListDto<UserInfoMainDto>>> GetFriendsAsync(Guid userId, int offset, int limit);
    Task<Result<UserListDto<UserInfoMainDto>>> GetEnemiesAsync(Guid userId, int offset, int limit);
    Task<Result<UserListDto<UserInfoMainDto>>> GetIncomingRequestsAsync(Guid userId, int offset, int limit);
    Task<Result<UserListDto<UserInfoMainDto>>> GetOutgoingRequestsAsync(Guid userId, int offset, int limit);
}
namespace Conductor.Dto.Users;

public record FriendResponseDto(
    Guid UserId,
    Guid FriendId,
    string Status);
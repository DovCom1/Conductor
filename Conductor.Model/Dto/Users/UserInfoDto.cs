namespace Conductor.Dto.Users;

public record UserInfoDto(
    Guid Id,
    string Uid,
    string Nickname,
    string Email,
    string AvatarUrl,
    string Gender,
    string Status,
    DateTime DateOfBirth,
    DateTime AccountCreationTime);

public record UserInfoMainDto(
    Guid Id,
    string Uid,
    string Nickname,
    string AvatarUrl,
    string Status);
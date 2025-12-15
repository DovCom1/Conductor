namespace Conductor.Dto.Users;

public record UserInfoDto(
    Guid Id,
    string Uid,
    string Nickname,
    string Email,
    string AvatarUrl,
    string Gender,
    string Status,
    DateOnly DateOfBirth,
    DateOnly AccountCreationTime);

public record UserInfoMainDto(
    Guid Id,
    string Uid,
    string Nickname,
    string AvatarUrl,
    string Status);
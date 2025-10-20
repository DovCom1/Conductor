namespace Conductor.Dto.Users;

public record UpdateUserInfoDto(
    Guid? Id,
    string? Uid,
    string? Nickname,
    string? Email,
    string? AvatarUrl,
    string? Gender,
    string? Status,
    DateTime? DateOfBirth,
    DateTime? AccountCreationTime);
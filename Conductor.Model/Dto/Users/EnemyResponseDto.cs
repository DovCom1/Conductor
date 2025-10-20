namespace Conductor.Dto.Users;

public record EnemyResponseDto(
    Guid UserId,
    Guid EnemyId);
namespace Conductor.Dto.Users;

public record UserListDto<T>(
    List<T> Data,
    int Offset,
    int Limit,
    int Total);
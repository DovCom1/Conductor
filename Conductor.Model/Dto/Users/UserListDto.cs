namespace Conductor.Dto.Users;

public record UserListDto<T>(
    IList<T> Data,
    int Offset,
    int Limit,
    int Total);
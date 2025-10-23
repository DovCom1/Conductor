using Conductor.Dto.Users;

namespace Conductor.Dto.Search;

public record SearchNicknameResponseDto(
    IList<UserInfoMainDto> Data,
    int Offset,
    int Limit,
    int Total);
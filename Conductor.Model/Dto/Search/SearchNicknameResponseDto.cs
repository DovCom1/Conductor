using Conductor.Dto.Users;

namespace Conductor.Dto.Search;

public record SearchNicknameResponseDto(
    List<UserInfoMainDto> Data,
    int Offset,
    int Limit,
    int Total);
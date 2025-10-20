namespace Conductor.Dto.Chat;

public record ChatInfoDto(
    string Name,
    Guid Id,
    Guid? AdminId,
    string AvatarUrl,
    ChatType Type);

public enum ChatType
{
    Private,
    Group
}
namespace Conductor.Dto.Chat;

public record ChatListDto(List<ChatMainInfoDto> Chats);

public record ChatMainInfoDto(
    Guid Id,
    string Name,
    string AvatarUrl);
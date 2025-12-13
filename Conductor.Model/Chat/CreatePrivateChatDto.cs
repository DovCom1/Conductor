namespace Conductor.Models.Chat;

public record CreatePrivateChatDto(string ReceiverId, string UserId, string Content);
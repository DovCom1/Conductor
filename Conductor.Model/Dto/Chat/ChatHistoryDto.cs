namespace Conductor.Dto.Chat;

public record ChatHistoryDto(
    Guid ChatId,
    int PageNumber,
    int PageSize,
    IList<ChatMessageDto>? Messages);
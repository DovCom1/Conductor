namespace Conductor.Dto.Chat;

public record ChatHistoryDto(
    Guid ChatId,
    int PageNumber,
    int PageSize,
    List<ChatMessageDto> Messages);
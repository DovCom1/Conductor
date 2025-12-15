namespace Conductor.Dto.Chat;

public record ChatMessageDto(
    Guid Id,
    Guid SenderId,
    string? Content,
    DateTime SentAt,
    DateTime? EditedAt,
    bool Deleted);
namespace Conductor.Dto.Chat;

public record ChatParticipantDto(Guid UserId, string Role, string Nickname);

public record ChatParticipantsDto(IList<ChatParticipantDto> Members);
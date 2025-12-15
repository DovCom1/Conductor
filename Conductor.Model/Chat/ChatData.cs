using Conductor.Dto.Chat;

namespace Conductor.Models.Chat;

public class ChatData
{
    public ChatInfoDto? Info { get; set; }
    public ChatParticipantsDto? Participants { get; set; }
    public ChatHistoryDto? History { get; set; }
}
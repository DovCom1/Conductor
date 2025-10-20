namespace Conductor.Models.Chat;

public class SendMessageRequest
{
    public Guid UserId { get; set; }
    public string? Content { get; set; }
}
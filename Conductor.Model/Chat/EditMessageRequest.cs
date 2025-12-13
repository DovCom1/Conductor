namespace Conductor.Models.Chat;

public class EditMessageRequest
{
    public Guid UserId { get; set; }
    public string? Content { get; set; }
}
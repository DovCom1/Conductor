using Conductor.Dto.Chat;
using Conductor.Models.Chat;

namespace Conductor.Models.Interfaces.Managers;

public interface IChatsManager
{
    Task<Result<ChatData>> GetChatDataAsync(Guid chatId, Guid userId, int pageNumber, int pageSize);
    Task<Result> SendMessageAsync(Guid chatId, SendMessageRequest request);
    Task<Result> EditMessageAsync(Guid chatId, Guid messageId, EditMessageRequest request);
    Task<Result> DeleteMessageAsync(Guid chatId, Guid messageId, Guid userId);
    Task<Result<ChatListDto>> GetChatsAsync(Guid userId);
    Task<Result> CreatePrivateChatAsync(CreatePrivateChatDto request);
}
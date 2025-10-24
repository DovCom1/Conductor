using Conductor.Dto.Chat;
using Conductor.Models.Chat;
using Conductor.Models.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ConductorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatsController: ControllerBase
{
    private readonly IChatsManager _chatsManager;

    public ChatsController(IChatsManager chatsManager)
    {
        _chatsManager = chatsManager;
    }

    [HttpGet("{chatId}")]
    public async Task<ActionResult<ChatData>> GetChatDataAsync(
        Guid chatId, 
        [FromQuery] int pageNumber, 
        [FromQuery] int pageSize)
    {
        var result = await _chatsManager.GetChatDataAsync(chatId, pageNumber, pageSize);
        return result.IsSuccess ? Ok(result.Data) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost("{chatId}")]
    public async Task<IActionResult> SendMessageAsync(Guid chatId, [FromBody] SendMessageRequest request)
    {
        var result = await _chatsManager.SendMessageAsync(chatId, request);
        return result.IsSuccess ? Ok() : StatusCode(result.StatusCode, result.Error);
    }
    
    [HttpPut("{chatId}/messages/{messageId}")]
    public async Task<IActionResult> EditMessageAsync(Guid chatId, Guid messageId, [FromBody] SendMessageRequest request)
    {
        var result = await _chatsManager.EditMessageAsync(chatId, messageId, request);
        return result.IsSuccess ? Ok() : StatusCode(result.StatusCode, result.Error);
    }
    
    [HttpDelete("{chatId}/messages/{messageId}/users/{userId}")]
    public async Task<IActionResult> DeleteMessageAsync(Guid chatId, Guid messageId, Guid userId)
    {
        var result = await _chatsManager.DeleteMessageAsync(chatId, messageId, userId);
        return result.IsSuccess ? Ok() : StatusCode(result.StatusCode, result.Error);
    }
    
    [HttpGet("users/{userId}")]
    public async Task<ActionResult<ChatListDto>> GetChatsAsync(Guid userId)
    {
        var result = await _chatsManager.GetChatsAsync(userId);
        return result.IsSuccess ? Ok(result.Data) : StatusCode(result.StatusCode, result.Error);
    }
}

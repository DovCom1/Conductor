using Conductor.Dto.Chat;
using Conductor.Dto.Settings;
using Conductor.Dto.Users;
using Conductor.Models;
using Conductor.Models.Chat;
using Conductor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Conductor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatsController: ControllerBase
{
    private readonly IRouteService _routeService;

    public ChatsController(IRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpGet("{chatId}")]
    public async Task<ActionResult<ChatData>> GetChatDataAsync(
        Guid chatId, 
        [FromQuery] int pageNumber, 
        [FromQuery] int pageSize)
    {
        var chatInfoTask = _routeService.GetAsync<ChatInfoDto>(Constants.ChatServiceName, $"/api/chats/{chatId}");
        var chatHistoryTask = _routeService.GetAsync<ChatHistoryDto>(Constants.ChatServiceName, $"/api/chats/{chatId}/messages?" +
            $"pageNumber={pageNumber}&pageSize={pageSize}");
        var chatParticipantsTask = _routeService.GetAsync<ChatParticipantsDto>(Constants.ChatServiceName, $"/api/chats/{chatId}/members");

        await Task.WhenAll(chatInfoTask, chatHistoryTask, chatParticipantsTask);

        if (!chatInfoTask.Result.IsSuccess)
        {
            return StatusCode(chatInfoTask.Result.StatusCode, $"An error occurred: {chatInfoTask.Result.Error}");
        }
        if (!chatHistoryTask.Result.IsSuccess)
        {
            return StatusCode(chatHistoryTask.Result.StatusCode, $"An error occurred: {chatHistoryTask.Result.Error}");
        }
        if (!chatParticipantsTask.Result.IsSuccess)
        {
            return StatusCode(chatParticipantsTask.Result.StatusCode, $"An error occurred: {chatParticipantsTask.Result.Error}");
        }

        var chatData = new ChatData()
        {
            Info = chatInfoTask.Result.Data,
            History = chatHistoryTask.Result.Data,
            Participants = chatParticipantsTask.Result.Data
        };
        
        return Ok(chatData);
    }

    [HttpPost("{chatId}")]
    public async Task<IActionResult> SendMessageAsync(Guid chatId, [FromBody] SendMessageRequest request)
    {
        var chatInfo = await _routeService.GetAsync<ChatInfoDto>(Constants.ChatServiceName, $"/api/chats/{chatId}");
        if (!chatInfo.IsSuccess)
        {
            return StatusCode(chatInfo.StatusCode, $"An error occurred: {chatInfo.Error}");
        }

        if (chatInfo.Data.Type is ChatType.Private)
        {
            var participants = await _routeService.GetAsync<ChatParticipantsDto>(Constants.ChatServiceName, $"/api/chats/{chatId}/members");

            if (!participants.IsSuccess)
                return StatusCode(participants.StatusCode, $"An error occurred: {participants.Error}");

            foreach (var participant in participants.Data.Members)
            {
                if (participant.UserId == request.UserId)
                    continue;

                var isEnemy = await _routeService.GetAsync<IsEnemyDto>(Constants.UserServiceName,
                    $"/api/users/{request.UserId}/enemies/{participant.UserId}/exists");

                if (!isEnemy.IsSuccess)
                    return StatusCode(isEnemy.StatusCode, $"An error occurred: {isEnemy.Error}");

                if (isEnemy.Data.Exists)
                {
                    var enemySettings = await _routeService.GetAsync<EnemySettingsDto>(Constants.SettingsServiceName,
                        $"/api/settings/{request.UserId}/enemies/{participant.UserId}");
                    if (!enemySettings.IsSuccess)
                        return StatusCode(enemySettings.StatusCode, $"An error occurred: {enemySettings.Error}");

                    if (enemySettings.Data.NotificationSettings is NotificationSettings.Nothing)
                        return BadRequest($"User {request.UserId} is blocked in chat {chatId}");
                }
            }
        }

        var sendResult = await _routeService.PostAsync<SendMessageRequest>(request,Constants.ChatServiceName, $"/api/chats/{chatId}/messages");

        if (!sendResult.IsSuccess)
        {
            return StatusCode(sendResult.StatusCode, $"Can`t send message. An error occurred: {sendResult.Error}");
        }

        return Ok();
    }
    
    [HttpPut("{chatId}/messages/{messageId}")]
    public async Task<IActionResult> EditMessageAsync(Guid chatId, Guid messageId, [FromBody] SendMessageRequest request)
    {
        var editResult = await _routeService.PutAsync<SendMessageRequest>(request, Constants.ChatServiceName,
            $"/api/chats/{chatId}/messages/{messageId}");
        return !editResult.IsSuccess ? StatusCode(editResult.StatusCode, $"An error occurred: {editResult.Error}") : Ok();
    }
    
    [HttpDelete("{chatId}/messages/{messageId}/users/{userId}")]
    public async Task<IActionResult> DeleteMessageAsync(Guid chatId, Guid messageId, Guid userId)
    {
        var deleteResult = await _routeService.DeleteAsync(Constants.ChatServiceName,
            $"/api/chats/{chatId}/messages/{messageId}/users/{userId}");
        return !deleteResult.IsSuccess ? StatusCode(deleteResult.StatusCode, $"An error occurred: {deleteResult.Error}") : Ok();
    }
    
    [HttpGet("users/{userId}")]
    public async Task<ActionResult<ChatListDto>> GetChatsAsync(Guid userId)
    {
        var chats = await _routeService.GetAsync<ChatListDto>(Constants.ChatServiceName,
            $"/api/chats/users/{userId}");
        return !chats.IsSuccess ? StatusCode(chats.StatusCode, $"An error occurred: {chats.Error}") : Ok(chats.Data);
    }
}

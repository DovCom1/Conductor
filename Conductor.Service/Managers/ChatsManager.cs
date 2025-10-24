﻿using Conductor.Dto.Chat;
using Conductor.Dto.Settings;
using Conductor.Dto.Users;
using Conductor.Model.Constants;
using Conductor.Model.Interfaces.Services;
using Conductor.Models;
using Conductor.Models.Chat;
using Conductor.Models.Interfaces.Managers;
using Microsoft.Extensions.Logging;

namespace Conductor.Managers;

public class ChatsManager : IChatsManager
{
    private readonly IRouteService _routeService;
    private readonly ILogger<ChatsManager> _logger;

    public ChatsManager(IRouteService routeService, ILogger<ChatsManager> logger)
    {
        _routeService = routeService;
        _logger = logger;
    }

    public async Task<Result<ChatData>> GetChatDataAsync(Guid chatId, int pageNumber, int pageSize)
    {
        var chatInfoTask = _routeService.GetAsync<ChatInfoDto>(Constants.ChatServiceName, $"/api/chats/{chatId}");
        var chatHistoryTask = _routeService.GetAsync<ChatHistoryDto>(
            Constants.ChatServiceName, 
            $"/api/chats/{chatId}/messages?pageNumber={pageNumber}&pageSize={pageSize}");
        var chatParticipantsTask = _routeService.GetAsync<ChatParticipantsDto>(
            Constants.ChatServiceName, 
            $"/api/chats/{chatId}/members");

        await Task.WhenAll(chatInfoTask, chatHistoryTask, chatParticipantsTask);

        var firstFailedResult = GetFirstFailedResponse(chatInfoTask.Result, chatHistoryTask.Result, chatParticipantsTask.Result);
        if (firstFailedResult is not null)
            return Result<ChatData>.Failure(firstFailedResult.Error, firstFailedResult.StatusCode);

        var chatData = new ChatData
        {
            Info = chatInfoTask.Result.Data,
            History = chatHistoryTask.Result.Data,
            Participants = chatParticipantsTask.Result.Data
        };
        
        return Result<ChatData>.Success(chatData);
    }

    public async Task<Result> SendMessageAsync(Guid chatId, SendMessageRequest request)
    {
        if (request is null)
            return Result.Failure("Request body is required", 400);

        var chatInfo = await _routeService.GetAsync<ChatInfoDto>(
            Constants.ChatServiceName, 
            $"/api/chats/{chatId}");

        if (!chatInfo.IsSuccess)
            return Result.Failure(chatInfo.Error, chatInfo.StatusCode);

        if (chatInfo.Data?.Type == ChatType.Private)
        {
            var validationResult = await ValidatePrivateChatMessageAsync(chatId, request);
            if (!validationResult.IsSuccess)
                return validationResult;
        }

        var sendResult = await _routeService.PostAsync<SendMessageRequest>(
            request, 
            Constants.ChatServiceName, 
            $"/api/chats/{chatId}/messages");

        return !sendResult.IsSuccess
            ? Result.Failure(sendResult.Error, sendResult.StatusCode)
            : Result.Success();
    }

    public async Task<Result> EditMessageAsync(Guid chatId, Guid messageId, SendMessageRequest request)
    {
        return await _routeService.PutAsync<SendMessageRequest>(
            request, 
            Constants.ChatServiceName,
            $"/api/chats/{chatId}/messages/{messageId}");
    }

    public async Task<Result> DeleteMessageAsync(Guid chatId, Guid messageId, Guid userId)
    {
        return await _routeService.DeleteAsync(
            Constants.ChatServiceName,
            $"/api/chats/{chatId}/messages/{messageId}/users/{userId}");
    }

    public async Task<Result<ChatListDto>> GetChatsAsync(Guid userId)
    {
        return await _routeService.GetAsync<ChatListDto>(
            Constants.ChatServiceName,
            $"/api/chats/users/{userId}");
    }

    private static Result? GetFirstFailedResponse(params Result[] responses)
    {
        return responses.FirstOrDefault(r => !r.IsSuccess);
    }

    private async Task<Result> ValidatePrivateChatMessageAsync(Guid chatId, SendMessageRequest request)
    {
        var participants = await _routeService.GetAsync<ChatParticipantsDto>(
            Constants.ChatServiceName, 
            $"/api/chats/{chatId}/members");

        if (!participants.IsSuccess)
            return Result.Failure(participants.Error, participants.StatusCode);

        foreach (var participant in participants.Data!.Members)
        {
            if (participant.UserId == request.UserId)
                continue;

            var isEnemy = await _routeService.GetAsync<IsEnemyDto>(
                Constants.UserServiceName, 
                $"/api/users/{request.UserId}/enemies/{participant.UserId}/exists");

            if (!isEnemy.IsSuccess)
                return Result.Failure(isEnemy.Error, isEnemy.StatusCode);

            if (isEnemy.Data?.Exists == true)
            {
                var enemySettings = await _routeService.GetAsync<EnemySettingsDto>(
                    Constants.SettingsServiceName, 
                    $"/api/settings/{request.UserId}/enemies/{participant.UserId}");

                if (!enemySettings.IsSuccess)
                    return Result.Failure(enemySettings.Error, enemySettings.StatusCode);

                if (enemySettings.Data?.NotificationSettings == NotificationSettings.Nothing)
                {
                    _logger.LogWarning(
                        "User {UserId} is blocked from sending messages in chat {ChatId} by user {ParticipantId}",
                        request.UserId, chatId, participant.UserId);
                    return Result.Failure($"User {request.UserId} is blocked in chat {chatId}", 400);
                }
            }
        }

        return Result.Success();
    }
}
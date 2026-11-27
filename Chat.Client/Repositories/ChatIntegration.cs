using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Chat.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Repositories;

public class ChatIntegration(
    HttpClient httpClient, 
    StorageService storageService
    ) : IChatIntegration
{
    public async Task<Tuple<HttpStatusCode, object>> GetUserChats()
    {
        await AddTokenHeader();
        string url = "api/users/user_id/chats";
        var result = await httpClient.GetAsync(url);
        var statusCode = result.StatusCode;
        object response;
        if (statusCode == HttpStatusCode.OK)
        {
            response = (await result.Content.ReadFromJsonAsync<List<ChatDto>>())!;
        }
        else
        {
            response = (await result.Content.ReadFromJsonAsync<string>())!;
        }

        return new(statusCode, response);  
    }

    public async Task<Tuple<HttpStatusCode, object>> GetUserChats(Guid toUserId)
    {
        string url = "api/users/user_id/chats";
        var result = await httpClient.PostAsJsonAsync(url, toUserId);
        var statusCode = result.StatusCode;
        object response;
        if (statusCode == HttpStatusCode.OK)
        {
            response = (await result.Content.ReadFromJsonAsync<ChatDto>())!;
        }
        else
        {
            response = (await result.Content.ReadFromJsonAsync<string>())!;
        }

        return new(statusCode, response); 
    }

    public async Task<Tuple<HttpStatusCode, object>> GetMessagesOfChat(Guid chatId)
    {
        await AddTokenHeader();
        var url = $"api/users/user_id/chats/{chatId}/Message";
        var result = await httpClient.GetAsync(url);
        var statusCode = result.StatusCode;
        object response;
        if (statusCode == HttpStatusCode.OK)
        {
            response = (await result.Content.ReadFromJsonAsync<List<MessageDto>>())!;
        }
        else
        {
            response = (await result.Content.ReadFromJsonAsync<string>())!;
        }
        return new(statusCode, response!);
    }

    public async Task<Tuple<HttpStatusCode, object>> SendTextMessage(Guid chatId, string text)
    {
        await AddTokenHeader();
        var url = $"api/users/user_id/chats/{chatId}/Message/{text}";
        var result = await httpClient.PostAsJsonAsync(url, text);
        var statusCode = result.StatusCode;
        var response = result.Content.ReadFromJsonAsync<object>();
        return new(statusCode, response);
    }

    private async Task AddTokenHeader()
    {
        string? token = await storageService.GetToken();

        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
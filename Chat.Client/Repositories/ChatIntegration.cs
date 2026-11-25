using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Chat.Client.Services;

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
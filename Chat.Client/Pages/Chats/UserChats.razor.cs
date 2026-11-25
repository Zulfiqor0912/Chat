using System.Net;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Pages.Chats;

public abstract class UserChatsBase : ComponentBase
{
    [Inject]
    private IChatIntegration ChatIntegration { get; set; }
    
    protected List<ChatDto> Chats { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var (statusCode, response) = await ChatIntegration.GetUserChats();
        if (statusCode == HttpStatusCode.OK)
        {
            Chats = (List<ChatDto>)response;
        }
    }
}
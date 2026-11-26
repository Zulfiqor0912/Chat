using System.Net;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Pages.Chats;

public abstract class SeeChatBase : ComponentBase
{
    [Inject]
    protected IChatIntegration? ChatIntegration { get; set; }
    [Inject]
    protected NavigationManager? NavigationManager { get; set; }
    [Parameter]
    public Guid ToUserId { get; set; }

    public ChatDto? ChatDto { get; set; }

    protected override async Task OnInitializedAsync()
    {
       await LoadChat(ToUserId);
    }

    private async Task LoadChat(Guid chatId)
    {
        var (statusCode, response) = await ChatIntegration!.GetUserChats(chatId);
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            NavigationManager!.NavigateTo("api/users/login");
        }
        if (statusCode != HttpStatusCode.OK)
        {
            
        }
        ChatDto = response as ChatDto ?? new ChatDto();
    }
}
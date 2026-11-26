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

    protected ChatDto? ChatDto { get; set; }
    protected string? Text { get; set; }
    protected List<MessageDto>? MessageDtos { get; set; } = new ();

    protected override async Task OnInitializedAsync()
    {
       await LoadChatInfo(ToUserId);
       await LoadMessagesOfChat(ChatDto!.Id);
    }

    private async Task LoadChatInfo(Guid chatId)
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

    private async Task LoadMessagesOfChat(Guid chatId)
    {
        var (statusCode, response) = await ChatIntegration!.GetMessagesOfChat(chatId);
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            NavigationManager?.NavigateTo("api/users/login");
        }
        else if (statusCode != HttpStatusCode.OK)
        {
            
        }
        MessageDtos = response as List<MessageDto> ?? new List<MessageDto>();
    }

    protected async Task SendMessage()
    {
        
    }

}
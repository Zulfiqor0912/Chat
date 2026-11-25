using System.Net;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Pages.Chats;

public abstract class UserChatsBase : ComponentBase
{
    [Inject]
    protected IChatIntegration? ChatIntegration { get; set; }
    [Inject]
    protected NavigationManager? NavigationManager { get; set; }
    
    protected List<ChatDto> Chats { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadUserChats();
    }

    private async Task LoadUserChats()
    {
        var errorMessage = string.Empty;
        try
        {
            var (statusCode, response) = await ChatIntegration!.GetUserChats();
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                NavigationManager!.NavigateTo("api/Users/login");
                return;
            }
            
            if(statusCode != HttpStatusCode.OK)
            {
                errorMessage = $"Chatlarni yuklab bo'lmadi{(int)statusCode}";
                return;
            }
            Chats = response as List<ChatDto> ?? new();
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
            throw;
        }
    }

    protected void SeeChat(Guid chatId)
    {
        NavigationManager!.NavigateTo($"/see-chat/{chatId}");
    }
}
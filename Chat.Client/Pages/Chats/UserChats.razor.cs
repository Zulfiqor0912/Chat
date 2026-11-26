using System.Net;
using System.Security.Claims;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Chat.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Chat.Client.Pages.Chats;

public abstract class UserChatsBase : ComponentBase
{
    [Inject]
    protected IChatIntegration? ChatIntegration { get; set; }
    [Inject]
    protected NavigationManager? NavigationManager { get; set; }
    [Inject]
    protected AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    
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

    protected async Task SeeChat(Guid chatId)
    {
        var chat = Chats.First(c => c.Id == chatId);
        var userId = await GetUserId();
        var userChat = chat.UserChats!.First(c => c.FirstUserId == userId);
        NavigationManager!.NavigateTo($"/see-chat/{userChat.LastUserId}");
    }

    private async Task<Guid> GetUserId()
    {
        var stateProvider = (CustomAuthHandler)AuthenticationStateProvider!;
        var state = await stateProvider.GetAuthenticationStateAsync();
        ClaimsPrincipal user = state.User;
        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        return Guid.Parse(userId);
    }
}
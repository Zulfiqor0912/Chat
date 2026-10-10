using Chat.Api.DTOs;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Pages.Account;

public partial class UsersBase : ComponentBase
{
    [Inject]
    IUserIntegration userIntegration { get; set; }
    [Inject]
    NavigationManager navigationManager { get; set; }
    protected List<UserDto>? Users { get; set; }

    public async Task GetAllusers()
    {
        var (statusCode, response) = await userIntegration.GetAllUsers();
        if (statusCode == System.Net.HttpStatusCode.OK)
        {
            Users = (List<UserDto>)response;
        }
        else if (statusCode == System.Net.HttpStatusCode.BadRequest 
            || statusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var errorMessage = (string)response;
            navigationManager.NavigateTo($"/error/{response}");
        }
    }
}

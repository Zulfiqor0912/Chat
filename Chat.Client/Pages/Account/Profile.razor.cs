using System.Net;
using Chat.Api.DTOs;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Pages.Account;

public class ProfileBase() : ComponentBase
{

    [Inject]
    private IUserIntegration UserIntegration { get; set; }

    public UserDto? User { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var (statusCode, response) = await UserIntegration.GetUserInfo();
        if (statusCode == HttpStatusCode.OK)
        {
            User = (UserDto)response!;
        }
        else
        {
            var error = (string)response!;
        }
    }
}
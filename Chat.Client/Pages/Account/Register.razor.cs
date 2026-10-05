using Chat.Client.Models.UserModels;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace Chat.Client.Pages.Account;

public partial class RegisterBase : ComponentBase
{
    [Inject]
    IUserIntegration userIntegration { get; set; }
    [Inject]
    NavigationManager navigationManager { get; set; }

    protected RegisterModel model = new RegisterModel();

    protected async Task RegisterClicked()
    {
        var (statusCode, response) = await userIntegration.Register(model);

        if (statusCode == HttpStatusCode.OK)
            navigationManager.NavigateTo("/account/login");
        else if (statusCode == HttpStatusCode.BadRequest)
            navigationManager.NavigateTo($"/error/{response}");
    }
}

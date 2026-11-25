using System.Net;
using Chat.Client.Constants;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Pages.Account;

public class ProfileBase : ComponentBase
{

    [Inject]
    public IUserIntegration UserIntegration { get; set; }
    [Inject] 
    public NavigationManager NavigationManager { get; set; }
    public UserDto? User { get; set; }
    protected string ImgUrl { get; set; } = string.Empty;
    protected byte? Age { get; set; }
    protected string? Bio { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var (statusCode, response) = await UserIntegration.GetUserInfo();
        if (statusCode == HttpStatusCode.OK)
        {
            User = response as UserDto;
            if (User!.ProfilePhotoData is {Length: > 0})
                ImgUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(User.ProfilePhotoData)}";
            else
                ImgUrl = UrlConstants.DefaultImgUrl;    
        }
        else
        {
            var error = (string)response!;
            ImgUrl = UrlConstants.DefaultImgUrl;
        }
    }

    protected async Task UpdateAge()
    {
        var (statusCode, response) = await UserIntegration.UpdateAge(Age!.Value);
        if (statusCode == HttpStatusCode.OK)
        {
            NavigationManager.Refresh(true);
        }
    }

    protected async Task UpdateBio()
    {
        var (statusCode, response) = await UserIntegration.UpdateBio(Bio!);
        if (statusCode == HttpStatusCode.OK)
        {
            NavigationManager.Refresh(true);
        }
    }
}
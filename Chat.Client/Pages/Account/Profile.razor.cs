using System.Net;
using Chat.Client.Constants;
using Chat.Client.DTOs;
using Chat.Client.Repositories.Contract;
using Microsoft.AspNetCore.Components;

namespace Chat.Client.Pages.Account;

public class ProfileBase() : ComponentBase
{

    [Inject]
    private IUserIntegration UserIntegration { get; set; }

    public UserDto? User { get; set; }
    protected string ImgUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var (statusCode, response) = await UserIntegration.GetUserInfo();
        if (statusCode == HttpStatusCode.OK)
        {
            User = (UserDto)response!;
            if (User.ProfilePhotoData != null)
                ImgUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(User.ProfilePhotoData)}";
            else
                ImgUrl = UrlConstants.DefaultImgUrl;    
        }
        else
        {
            var error = (string)response!;
        }
    }
}
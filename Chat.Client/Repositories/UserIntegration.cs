using Chat.Client.Models;
using Chat.Client.Models.UserModels;
using Chat.Client.Repositories.Contract;
using System.Net;
using System.Net.Http.Json;

namespace Chat.Client.Repositories;

public class UserIntegration(HttpClient httpClient) : IUserIntegration
{
    public async Task<Tuple<HttpStatusCode, string>> Login(LoginModel model)
    {
        string url = "api/Users/login";
        var result = await httpClient.PostAsJsonAsync(url, model);
        var statusCode = result.StatusCode;
        var response = await result.Content.ReadAsStringAsync();
        return new(statusCode, response);
    }

    public async Task<Tuple<HttpStatusCode, string>> Register(RegisterModel model)
    {
        string url = "api/Users/register";
        var result = await httpClient.PostAsJsonAsync(url, model);
        var statusCode = result.StatusCode;
        var response = await result.Content.ReadAsStringAsync();
        return new(statusCode, response);
    }
}

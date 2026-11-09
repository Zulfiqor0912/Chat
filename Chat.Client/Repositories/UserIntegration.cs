using Chat.Api.DTOs;
using Chat.Client.Models;
using Chat.Client.Models.UserModels;
using Chat.Client.Repositories.Contract;
using D20Tek.Blazor.BrowserStorage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Chat.Client.Services;

namespace Chat.Client.Repositories;

public class 
    UserIntegration(
        HttpClient httpClient, 
        ILocalStorageService localStorageService,
        StorageService storageService) : IUserIntegration
{
    public async Task<Tuple<HttpStatusCode, object>> GetAllUsers()
    {
        var url = "api/Users";
        var storageResult = await localStorageService.GetAsync<string>("token");

        if (storageResult.IsSuccess)
        {
            var token = storageResult.Value;
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
        var result = await httpClient.GetAsync(url);
        var statusCode = result.StatusCode;
        if (statusCode == HttpStatusCode.OK)
        {
            var users = await result.Content.ReadFromJsonAsync<List<UserDto>>();
            return new(statusCode, users!);
        }
        else if (statusCode == HttpStatusCode.BadGateway)
        {
            var response = await result.Content.ReadAsStringAsync();
            return new(statusCode, response);
        }
        else if (statusCode == HttpStatusCode.Unauthorized)
            return new(statusCode, "Unauthorized");
        else return new(statusCode, "Something went wrong");
    }

    public async Task<Tuple<HttpStatusCode, object>> GetUserInfo()
    {
        string? token = await storageService.GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }
        var url = "api/users/profile";
        var result = await httpClient.GetAsync(url);
        var statusCode = result.StatusCode;
        object? response;
        if (statusCode == HttpStatusCode.OK)
        {   
            response = await result.Content.ReadFromJsonAsync<UserDto>();
        }
        else if (statusCode == HttpStatusCode.BadRequest)
        {
            response = await result.Content.ReadFromJsonAsync<string>();
        }
        else
        {
            response = await result.Content.ReadFromJsonAsync<string>();
        }

        return new Tuple<HttpStatusCode, object>(statusCode, response!);
    }

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

using D20Tek.Blazor.BrowserStorage;

namespace Chat.Client.Services;

public class StorageService(ILocalStorageService localStorageService)
{
    private const string Key = "token";
    public async Task SetToken(string token)
    {
        await localStorageService.SetAsync(Key, token);
    }

    public async Task<string> GetToken()
    {
        var token = await localStorageService.GetAsync<string>(Key);
        return token.Value!;
    }
}

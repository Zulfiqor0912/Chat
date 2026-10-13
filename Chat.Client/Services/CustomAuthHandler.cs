using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Chat.Client.Services;

public class CustomAuthHandler : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ClaimsPrincipal> SetClaims(string? userId, string? username, string? role)
    {
        var check = string.IsNullOrEmpty(userId)
            || string.IsNullOrEmpty(username)
            || string.IsNullOrEmpty(role);

        if (!check)
            return new ClaimsPrincipal();

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId!),
                new Claim(ClaimTypes.Name, username!),
                new Claim(ClaimTypes.Role, role!)
            }, authenticationType: "JwtAuth"
            )); 
        return claimsPrincipal;
    }

    public async Task ParseJwtToken()
    {
        
    }
}

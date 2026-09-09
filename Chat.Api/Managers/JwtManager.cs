using Chat.Api.Entities;
using Chat.Api.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chat.Api.Managers;

public class JwtManager
{
    private IConfiguration configuration { get; set; }
    private JwtParameters jwtParameters { get; set; }
    public JwtManager(IConfiguration configuration)
    {
        this.configuration = configuration;
        jwtParameters = configuration.GetSection("JwtParameters").Get<JwtParameters>()!;
    }
    public string GenerateToken(User user)
    {
        var key = System.Text.Encoding.UTF32.GetBytes(jwtParameters.Key);
        var signingKey = new SigningCredentials(new SymmetricSecurityKey(key), "Hs256");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var security = new JwtSecurityToken(
            issuer: jwtParameters.Issuer, 
            audience: jwtParameters.Audience, 
            signingCredentials: signingKey, 
            claims: claims);

        var token = new JwtSecurityTokenHandler()
            .WriteToken(security);
        return token;
    }
}

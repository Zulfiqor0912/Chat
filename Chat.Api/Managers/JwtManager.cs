using Chat.Api.Entities;
using Chat.Api.Helpers;
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

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes)
        }
    }
}

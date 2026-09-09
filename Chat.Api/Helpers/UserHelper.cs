using System.Security.Claims;

namespace Chat.Api.Helpers;

public class UserHelper(IHttpContextAccessor httpContextAccessor)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public Guid GetUserId()
    {
        var userId = Guid.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));
        return userId;
    }
}

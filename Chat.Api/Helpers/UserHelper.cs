using System.Security.Claims;

namespace Chat.Api.Helpers;

public class UserHelper(IHttpContextAccessor httpContextAccessor)
{
    public Guid GetUserId()
    {
        var userId = Guid.Parse(httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));
        return userId;
    }
}

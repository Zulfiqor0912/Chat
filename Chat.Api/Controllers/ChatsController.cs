using Chat.Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/users/{userId:guid}/[controller]")]
[ApiController]
public class ChatsController(ChatManager chatManager) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserChats(Guid userId)
    {
        var chats = await chatManager.GetAllChatsOfUser(userId);
        return Ok(chats);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEnterChat(Guid userId, [FromBody] Guid toUserId)
    {
        var chat = await chatManager.AddOrEnterChat(userId, toUserId);
        return Ok(chat);
    }
}

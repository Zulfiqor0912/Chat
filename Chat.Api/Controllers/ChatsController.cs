using Chat.Api.Managers;
using Chat.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/users/{userId:guid}/[controller]")]
[ApiController]
public class ChatsController(ChatManager chatManager) : ControllerBase
{

    //[HttpGet]//for only admin
    //public async Task<IActionResult> GetAllChats()
    //{
    //    var chats = await chatManager.GetAllChats();
    //    return Ok(chats);
    //}
        
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

    //[HttpPut]
    //public async Task<IActionResult> UpdateChat([FromBody] UpdateChatModel model)
    //{
    //    var result = await chatManager.UpdateChat(model);
    //    return Ok(result);
    //}
}

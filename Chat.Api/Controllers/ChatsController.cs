using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Authorize]
[Route("api/users/user_id/[controller]")]
[ApiController]
public class ChatsController(
    ChatManager chatManager,
    UserHelper userHelper) : ControllerBase
{
    private readonly UserHelper _userHelper = userHelper;
    private readonly ChatManager _chatManager = chatManager;

    [HttpGet("/api/chats/all")]//for only admin
    public async Task<IActionResult> GetAllChats()
    {
        var chats = await _chatManager.GetAllChats();
        return Ok(chats);
    }
    [HttpGet]
    public async Task<IActionResult> GetChatsOfUser()
    {
        var userId = _userHelper.GetUserId();
        var chats = await _chatManager.GetAllChatsOfUser(userId);
        return Ok(chats);
    }
    [HttpPost]
    public async Task<IActionResult> AddOrEnterChat([FromBody] Guid toUserId)
    {
        var userId = _userHelper.GetUserId();
        var chat = await _chatManager.AddOrEnterChat(userId, toUserId);
        return Ok(chat);
    }
    [HttpDelete("{chatId:guid}")]
    public async Task<IActionResult> DeleteChat(Guid chatId)
    {
        try
        {
            var result = await _chatManager.DeleteChat(chatId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    //[HttpPut]
    //public async Task<IActionResult> UpdateChat([FromBody] UpdateChatModel model)
    //{
    //    var result = await chatManager.UpdateChat(model);
    //    return Ok(result);
    //}

    
}

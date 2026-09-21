using Chat.Api.Constants;
using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models;
using Chat.Api.Models.MessageModels;
using Chat.Api.Utility.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

//[Authorize]
[Route("api/users/user_id/chats/{chatId}/[controller]")]
[ApiController]
public class MessageController(
    MessageManager messageManager,
    UserHelper userHelper) : ControllerBase
{
    
    [Authorize(Roles = UserConstants.Admin)] //only admin
    [Route("/api/messages")]
    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var messages = await messageManager.GetMessages();
        return Ok(messages);
    }
    
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpGet]
    public async Task<IActionResult> GetMessagesOfChat(Guid chatId)
    {
        var messages = await messageManager.GetMessageByChatId(chatId);
        return Ok(messages);
    }
    
    [Authorize(Roles = UserConstants.Admin)]
    [Route("/api/messages/{messageId:int}")]
    [HttpGet]
    public async Task<IActionResult> GetMessageById(int messageId)
    {
        var message = await messageManager.GetMessageById(messageId);
        return Ok(message);
    }
    
    [Authorize(Roles = UserConstants.Admin)]
    [HttpGet("{messageId:int}")]
    public async Task<IActionResult> GetMessageById(Guid chatId, int messageId)
    {
        var message = await messageManager.GetMessageById(chatId, messageId);
        return Ok(message);
    }
    
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpPost("send-text-message")]
    public async Task<IActionResult> SendTextMessage(Guid chatId, SendTextMessageModel model)
    {
        var userId = userHelper.GetUserId();
        var result = await messageManager.SendTextMessage(userId, chatId, model);
        return Ok(result);
    }
    
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpPost("send-file-message")]
    public async Task<IActionResult> SendFileMessage(Guid chatId, FileModel model)
    { 
        var userId = userHelper.GetUserId();
        var result = await messageManager.SendFileMessage(userId, chatId, model);
        return Ok(result);
    }
}

using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models;
using Chat.Api.Models.MessageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Authorize]
[Route("api/users/user_id/chats/{chatId}/[controller]")]
[ApiController]
public class MessageController(
    MessageManager messageManager,
    UserHelper userHelper) : ControllerBase
{
    private readonly MessageManager _messageManger = messageManager;
    private readonly UserHelper _userHelper = userHelper;

    [Route("/api/messages")]
    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var messages = await _messageManger.GetMessages();
        return Ok(messages);
    }
    [HttpGet]
    public async Task<IActionResult> GetMessagesOfChat(Guid chatId)
    {
        var messages = await _messageManger.GetMessageByChatId(chatId);
        return Ok(messages);
    }
    [Route("/api/messages/{messageId:int}")]
    [HttpGet]
    public async Task<IActionResult> GetMessageById(int messageId)
    {
        var message = await _messageManger.GetMessageById(messageId);
        return Ok(message);
    }
    [HttpGet("{messageId:int}")]
    public async Task<IActionResult> GetMessageById(Guid chatId, int messageId)
    {
        var message = await _messageManger.GetMessageById(chatId, messageId);
        return Ok(message);
    }
    [HttpPost("send-text-message")]
    public async Task<IActionResult> SendTextMessage(Guid chatId, SendTextMessageModel model)
    {
        var userId = _userHelper.GetUserId();
        var result = await _messageManger.SendTextMessage(userId, chatId, model);
        return Ok(result);
    }
    [HttpPost("send-file-message")]
    public async Task<IActionResult> SendFileMessage(Guid chatId, FileModel model)
    { 
        var userId = _userHelper.GetUserId();
        var result = await _messageManger.SendFileMessage(userId, chatId, model);
        return Ok(result);
    }
}

using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Extentions;
using Chat.Api.Helpers;
using Chat.Api.Models;
using Chat.Api.Models.MessageModels;
using Chat.Api.Repositories.Interfaces;
using Mapster;

namespace Chat.Api.Managers;

public class MessageManager(
    IUnitOfWork unitOfWork,
    IHostEnvironment hostEnvironment)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHostEnvironment _ihostEnvironment = hostEnvironment;


    public async Task<List<MessageDto>> GetMessages()
    {
        var messages = await _unitOfWork.MessageRepository.GetMessage();
        if (messages == null)
        {
            throw new MessageNotFoundException();
        }
        else
        {
            var dtos = messages.ParseMessagesToDto();
            return dtos;
        }
    }
    public async Task<List<MessageDto>> GetMessageByChatId(Guid chatId)
    {
        var messages = await _unitOfWork.MessageRepository.GetMessageByChatId(chatId);
        if (messages == null)
        {
            throw new MessageNotFoundException();
        }
        else
        {
            var dtos = ParseToDtoExtension.ParseMessagesToDto(messages);
            return dtos;
        }
    }
    public async Task<MessageDto> GetMessageById(int messageId)
    {
        var message = await _unitOfWork.MessageRepository.GetMessageById(messageId);
        if (message == null)
        {
            throw new MessageNotFoundException();
        }
        else
        {
            var dto = ParseToDtoExtension.ParseMessageToDto(message);
            return dto;
        }
    }
    public async Task<MessageDto> GetMessageById(Guid chatId, int messageId)
    {
        var message = await _unitOfWork.MessageRepository.GetMessageById(chatId, messageId);
        if (message == null)
            throw new MessageNotFoundException();
        else
        {
            var dto = ParseToDtoExtension.ParseMessageToDto(message);
            return dto;
        }
    }
    public async Task<MessageDto> SendTextMessage(Guid userId,Guid chatId, SendTextMessageModel model)
    {
        await _unitOfWork.UserChatRepository.GetUserChat(userId, chatId);
        var user = await _unitOfWork.UserRepository.GetUserById(userId);

        var message = new Message
        {
            Text = model.Text,
            FromUserId = userId,
            IsEdited = false,
            FromUserName = user.Username,
            ChatId = chatId
        };
        await _unitOfWork.MessageRepository.Addmessage(message);
        return message.ParseMessageToDto();
    }
    public async Task<MessageDto> SendFileMessage(Guid userId, Guid chatId, FileModel model)
    {
        var user = await _unitOfWork.UserRepository.GetUserById(userId);
        await _unitOfWork.UserChatRepository.GetUserChat(userId, chatId);

        var ms = new MemoryStream();
        await model.File.CopyToAsync(ms);
        var data = ms.ToArray();
        var filePath = GetFilePath();
        await File.WriteAllBytesAsync(filePath, data);

        var content = new Content
        {
            Url = filePath,
            Type = model.File.ContentType,
        };

        var contents = new List<Content>();
        contents.Add(content);

        var message = new Message
        {
            Text = string.Empty,
            FromUserId = userId,
            FromUserName = user.Username,
            ChatId = chatId,
            Contents = contents
        };

        await _unitOfWork.MessageRepository.Addmessage(message);
        return message.ParseMessageToDto();
    }

    private string GetFilePath()
    {
        var generalPath = _ihostEnvironment.ContentRootPath;
        var name = Guid.NewGuid();
        var fileName = generalPath +"\\wwwroot\\MessageFiles"+ name;
        return fileName;
    }
}

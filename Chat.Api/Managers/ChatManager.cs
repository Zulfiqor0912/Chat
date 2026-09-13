using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Extentions;
using Chat.Api.Helpers;
using Chat.Api.Models;
using Chat.Api.Repositories.Interfaces;

namespace Chat.Api.Managers;

public class ChatManager(
    IUnitOfWork unitOfWork,
    UserHelper userHelper)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserHelper _userHelper = userHelper;
    public async Task<List<ChatDto>> GetAllChats() //for admin
    {
        var chats = await _unitOfWork.ChatRepository.GetAllChats();
        var dtos = chats.ParseChatDtos();
        return dtos;
    }

    public async Task<List<ChatDto>> GetAllChatsOfUser(Guid userId)
    {
        var chatsOfUser = await _unitOfWork.ChatRepository.GetAllChatsOfUser(userId);
        return chatsOfUser.ParseChatDtos();
    }

    public async Task<ChatDto> GetUserChatById(Guid userId, Guid chatId)
    {
        var chat = await _unitOfWork.ChatRepository.GetChatById(userId, chatId);
        return chat.ParseChatToDto();
    }

    public async Task<ChatDto> AddOrEnterChat(Guid fromUserId, Guid toUserId)
    {
        var (check, chat) = await _unitOfWork.ChatRepository.CheckChatExist(fromUserId, toUserId);

        if (check)
            return chat?.ParseChatToDto()!;

        var fromUser = await _unitOfWork.UserRepository.GetUserByid(fromUserId);
        var toUser = await _unitOfWork.UserRepository.GetUserByid(toUserId);

        List<string> chatNames = new()
        {
            StaticHelper.GetFullName(fromUser.FirsName, fromUser.LastName),
            StaticHelper.GetFullName(toUser.FirsName, toUser.LastName)
        };

        chat = new Entities.Chat
        {
            ChatNames = chatNames
        };

        await _unitOfWork.ChatRepository.AddChat(chat);

        var fromUserChat = new UserChat()
        {
            FirstUserId = fromUserId,
            LastUserId = toUserId,
            ChatId = chat.Id
        };

        await _unitOfWork.UserChatRepository.AddUserChat(fromUserChat);

        var toUserChat = new UserChat()
        {
            FirstUserId = toUserId,
            ChatId = chat.Id,
            LastUserId = fromUserId

        };

        await _unitOfWork.UserChatRepository.AddUserChat(toUserChat);
        return chat.ParseChatToDto();
    }
    public async Task<string> DeleteChat(Guid chatId)
    {
        var userId = _userHelper.GetUserId();
        var chat = await _unitOfWork.ChatRepository.GetChatById(userId, chatId);
        await _unitOfWork.ChatRepository.DeleteChatById(chat);
        return "Delete successfuly!!!";
    }
    //public Task UpdateChat(UpdateChatModel model)
    //{
    //    var chat = new Entities.Chat()
    //    {
            
    //    }
    //}
}

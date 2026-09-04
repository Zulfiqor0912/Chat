using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Extentions;
using Chat.Api.Helpers;
using Chat.Api.Models;
using Chat.Api.Repositories.Interfaces;

namespace Chat.Api.Managers;

public class ChatManager(IUnitOfWork unitOfWork)
{
    public async Task<List<ChatDto>> GetAllChats() //for admin
    {
        var chats = await unitOfWork.ChatRepository.GetAllChats();
        return chats.ParseChatDtos();
    }

    public async Task<List<ChatDto>> GetAllChatsOfUser(Guid userId)
    {
        var chatsOfUser = await unitOfWork.ChatRepository.GetAllChatsOfUser(userId);
        return chatsOfUser.ParseChatDtos();
    }

    public async Task<ChatDto> GetUserChatById(Guid userId, Guid chatId)
    {
        var chat = await unitOfWork.ChatRepository.GetUserChatById(userId, chatId);
        return chat.ParseChatToDto();
    }

    public async Task<ChatDto> AddOrEnterChat(Guid fromUserId, Guid toUserId)
    {
        var (check, chat) = await unitOfWork.ChatRepository.CheckChatExist(fromUserId, toUserId);

        if (check)
            return chat?.ParseChatToDto()!;

        var fromUser = await unitOfWork.UserRepository.GetUserByid(fromUserId);
        var toUser = await unitOfWork.UserRepository.GetUserByid(toUserId);

        List<string> chatNames = new()
        {
            StaticHelper.GetFullName(fromUser.FirsName, fromUser.LastName),
            StaticHelper.GetFullName(toUser.FirsName, toUser.LastName)
        };

        chat = new Entities.Chat
        {
            ChatNames = chatNames
        };

        await unitOfWork.ChatRepository.AddChat(chat);

        var fromUserChat = new UserChat()
        {
            FirstUserId = fromUserId,
            LastUserId = toUserId,
            ChatId = chat.Id
        };

        await unitOfWork.UserChatRepository.AddUserChat(fromUserChat);

        var toUserChat = new UserChat()
        {
            FirstUserId = toUserId,
            ChatId = chat.Id,
            LastUserId = fromUserId

        };

        await unitOfWork.UserChatRepository.AddUserChat(toUserChat);
        return chat.ParseChatToDto();
    }
    //public Task UpdateChat(UpdateChatModel model)
    //{
    //    var chat = new Entities.Chat()
    //    {
            
    //    }
    //}
}

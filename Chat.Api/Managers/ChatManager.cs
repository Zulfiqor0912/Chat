using Chat.Api.DTOs;
using Chat.Api.Extentions;
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

    public async Task AddOrEnterChat(Guid fromUserId, Guid toUserId)
    {
        var check = await unitOfWork.ChatRepository.CheckChatExist(fromUserId, toUserId);
    }
}

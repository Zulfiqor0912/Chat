using Chat.Api.DTOs;
using Chat.Api.Repositories.Interfaces;

namespace Chat.Api.Managers;

public class ChatManager(IUnitOfWork unitOfWork)
{
    public async Task<List<ChatDto>> GetAllChats() //for admin
    {
        var chats = await unitOfWork.ChatRepository.GetAllChats();
        foreach (var item in chats)
        {
            
        }
    }
}

using Chat.Api.Entities;

namespace Chat.Api.Repositories.Interfaces;

public interface IChatRepository
{
    public Task<List<Entities.Chat>> GetAllChats();
    public Task<List<Entities.Chat>> GetAllUserChats(Guid userId);
    public Task<Entities.Chat> GetUserChatById(Guid userId, Guid chatId);
    public Task UpdateChat(Entities.Chat chat);
    public Task DeleteChatById(Entities.Chat chat);
    public Task ArchiveChatById(Entities.Chat chat);
    public Task AddUserChat(Entities.Chat chat);
}

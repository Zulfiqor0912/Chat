using Chat.Api.Entities;

namespace Chat.Api.Repositories.Interfaces;

public interface IChatRepository
{
    public Task<List<Entities.Chat>> GetAllChats();
}

using Chat.Api.Context;
using Chat.Api.Exceptions;
using Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories;

public class ChatRepository(ChatDbContext dbContext) : IChatRepository
{
    public async Task<List<Entities.Chat>> GetAllChats()
    {
        var chats = await dbContext.Chats.AsNoTracking().ToListAsync();
        return chats is null ? throw new ChatNotFoundException() : chats;
    }
}

using Chat.Api.Context;
using Chat.Api.Exceptions;
using Chat.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories;

public class ChatRepository(ChatDbContext dbContext) : IChatRepository
{
    public async Task AddUserChat(Entities.Chat chat)
    {
        await dbContext.Chats.AddAsync(chat);
        await dbContext.SaveChangesAsync();
    }

    public async Task ArchiveChatById(Entities.Chat chat)
    {
        dbContext.Chats.Update(chat);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteChatById(Entities.Chat chat)
    {
        dbContext.Chats.Remove(chat);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Entities.Chat>> GetAllChats()
    {
        var chats = await dbContext.Chats.AsNoTracking().ToListAsync();
        return chats is null ? throw new ChatNotFoundException() : chats;
    }

    public async Task<List<Entities.Chat>> GetAllUserChats(Guid userId)
    {
        var userChats = await dbContext.UserChats
            .Where(uc => uc.UserId == userId)
            .Include(uc => uc.Chat)
            .ToListAsync();

        var chats = userChats.Select(uc => uc.Chat).ToList();
        return chats!;
    }

    public async Task<Entities.Chat> GetUserChatById(Guid userId, Guid chatId)
    {
        var userChat = await dbContext.UserChats
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChatId == chatId);

    }

    public async Task UpdateChat(Entities.Chat chat)
    {
       dbContext.Chats.Update(chat);
        await dbContext.SaveChangesAsync();
    }
}

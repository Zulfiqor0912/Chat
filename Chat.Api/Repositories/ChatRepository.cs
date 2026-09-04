using Chat.Api.Context;
using Chat.Api.Exceptions;
using Chat.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories;

public class ChatRepository(ChatDbContext dbContext) : IChatRepository
{
    public async Task AddChat(Entities.Chat chat)
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

    public async Task<List<Entities.Chat>> GetAllChatsOfUser(Guid userId)
    {
        var userChats = await dbContext.UserChats
            .Where(uc => uc.FirstUserId == userId)
            .Include(uc => uc.Chat)
            .ToListAsync();

        var chats = userChats.Select(uc => uc.Chat).ToList();
        return chats is null ? new List<Entities.Chat>() : chats!;
    }

    public async Task<Entities.Chat> GetUserChatById(Guid userId, Guid chatId)
    {
        var userChat = await dbContext.UserChats
            .Include(uc => uc.Chat)
                .ThenInclude(c => c!.UserChats)
            .Include(uc => uc.Chat)
                .ThenInclude(c => c!.Messages)
            .FirstOrDefaultAsync(uc => uc.FirstUserId == userId && uc.ChatId == chatId);
        var chat = userChat?.Chat;
        return chat!;
    }

    public async Task UpdateChat(Entities.Chat chat)
    {
        dbContext.Chats.Update(chat);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Tuple<bool, Entities.Chat?>> CheckChatExist(Guid fromUserId, Guid toUserId)
    {
        var userChat = await dbContext.UserChats.FirstOrDefaultAsync(uc => uc.FirstUserId == fromUserId && uc.LastUserId == toUserId);

        if (userChat != null)
        {
            var chat = await GetUserChatById(userChat.FirstUserId, userChat.ChatId);
            return new(true, chat);
        }
        return new(false, null);
    }
}

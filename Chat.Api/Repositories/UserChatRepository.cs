using Chat.Api.Context;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories;

public class UserChatRepository(ChatDbContext dbContext) : IUserChatRepository
{
    public async Task AddUserChat(UserChat userChat)
    {
        await dbContext.UserChats.AddAsync(userChat);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserChat(UserChat userChat)
    {
        dbContext.UserChats.Remove(userChat);   
        await dbContext.SaveChangesAsync();
    }

    public async Task GetUserChat(Guid userId, Guid chatId)
    {
        var userChat = await dbContext.UserChats
            .SingleOrDefaultAsync(u => u.FirstUserId == userId
                                         && u.ChatId == chatId);

        if (userChat is null)
            throw new ChatNotFoundException();
    }
}

using Chat.Api.Context;
using Chat.Api.Entities;
using Chat.Api.Repositories.Interfaces;

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
}

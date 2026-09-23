using Chat.Api.Context;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories;

public class UserRepository(ChatDbContext dbContext) : IUserRepository
{
    public async Task AddUser(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await dbContext.Users.AsNoTracking().ToListAsync();
        return users is null ? throw new UserNotFoundException() : users;
    }

    public async Task<User> GetUserById(Guid id)
    {
        var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        return user is null ? throw new UserNotFoundException() : user;
    }

    public async Task<User>? GetUserByUsername(string username)
    {
        var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        return user!;
    }

    public async Task UpdateUser(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }
}

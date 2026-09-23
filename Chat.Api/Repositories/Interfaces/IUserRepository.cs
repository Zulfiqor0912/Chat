using Chat.Api.Entities;
using Chat.Api.Models;

namespace Chat.Api.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<User>> GetAllUsers();
    public Task<User> GetUserById(Guid id);
    public Task<User>? GetUserByUsername(string username);
    public Task AddUser(User user);
    public Task UpdateUser(User user);
    public Task DeleteUser(User user);
}

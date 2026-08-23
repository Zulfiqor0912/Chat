using Chat.Api.Entities;

namespace Chat.Api.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<User>> GetAllUserAsync();
    public Task<User> GetUserByid(Guid id);
    public Task<User>? GetUserByUsername(string username);
    public Task AddUser(User user);
    public Task UpdateUserById(User user);
    public Task DeleteUser(User user);
}

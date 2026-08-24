using Chat.Api.Extentions;
using Chat.Api.Models;
using Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Chat.Api.Managers;

public class UserManager(IUserRepository userRepository)
{
    public async Task GetAllUsers(CreateUserModel model)
    {
        var users = await userRepository.GetAllUsers();
        return users.ParseUserDtos(users);
    }
}

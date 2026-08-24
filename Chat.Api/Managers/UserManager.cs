using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Extentions;
using Chat.Api.Models;
using Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Chat.Api.Managers;

public class UserManager(IUserRepository userRepository)
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await userRepository.GetAllUsers();
        return users.ParseUserDtos();
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await userRepository.GetUserByid(id);
        return user.ParseUserToDto();
    }

    public async Task<UserDto> GetUserByUsername(string username)
    {
        var user = await userRepository.GetUserByUsername(username)!;
        return user.ParseUserToDto();
    }

    public async Task Register(CreateUserModel model)
    {
        await CheckForExist(model.Username);

        var user = new User()
        {
            FirsName = model.FirsName,
            LastName = model.LastName,
            Username = model.Username,
            Gender = model.Gender,

        }

    }

    private async Task CheckForExist(string username)
    {
        var user = await userRepository.GetUserByUsername(username)!;
        if (user is null) 
            throw new UserExistException();
    }
}

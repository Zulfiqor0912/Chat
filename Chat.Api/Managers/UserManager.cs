using Chat.Api.Constants;
using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Extentions;
using Chat.Api.Models;
using Chat.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
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
    public async Task<UserDto> Register(CreateUserModel model)
    {
        await CheckForExist(model.Username);

        
        var user = new User()
        {
            FirsName = model.FirsName,
            LastName = model.LastName,
            Username = model.Username,
            Age = model.Age,
            Gender = GetGender(model.Gender)

        };

        var passworHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        user.PasswrodHash = passworHash;
        await userRepository.AddUser(user);
        return user.ParseUserToDto();

    }
    public async Task<string> Login(LoginModel model)
    {
        var user = await userRepository.GetUserByUsername(model.Username)!;
        if (user is null) 
            throw new Exception("Username is invalid");
        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswrodHash, model.Passwor);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid password");
        return "Login successfully";
    }
    private async Task CheckForExist(string username)
    {
        var user = await userRepository.GetUserByUsername(username)!;
        if (user is null)
            throw new UserExistException();
    }
    private string GetGender(string gender)
    {
        return gender.ToUpper() == UserConstants.Famele ? gender : UserConstants.Male;
    }
}

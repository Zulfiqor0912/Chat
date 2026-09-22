using Chat.Api.Constants;
using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Extentions;
using Chat.Api.Helpers;
using Chat.Api.Models.UserModels;
using Chat.Api.Repositories.Interfaces;
using Chat.Api.Utility.Enums;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Chat.Api.Managers;

public class UserManager(IUnitOfWork unitOfWork, JwtManager jwtManager)
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await unitOfWork.UserRepository.GetAllUsers();
        return users.ParseUserDtos();
    }
    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await unitOfWork.UserRepository.GetUserByid(id);
        return user.ParseUserToDto();
    }
    public async Task<UserDto> GetUserByUsername(string username)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsername(username)!;
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

        if (user.Username == "admin-admin")
        {
            user.Role = UserConstants.Admin;
        }

        var passworHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        user.PasswrodHash = passworHash;
        await unitOfWork.UserRepository.AddUser(user);
        return user.ParseUserToDto();

    }
    public async Task<string> Login(LoginModel model)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsername(model.Username)!;
        if (user is null)
            throw new Exception("Username is invalid");
        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswrodHash, model.Passwor);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid password");

        if (string.IsNullOrEmpty(user.Role))
        {
            user.Role = UserConstants.User;
            await unitOfWork.UserRepository.UpdateUser(user);
        }
        
        var token = jwtManager.GenerateToken(user);
        return token; 
    }
    public async Task<byte[]> AddOrUpdatePhoto(Guid userId, IFormFile file)
    {
        var user = await unitOfWork.UserRepository.GetUserByid(userId);

        StaticHelper.IsPhoto(file);
        var data = StaticHelper.PhotoFileToArray(file);

        user.ProfilePhotoData = data;
        await unitOfWork.UserRepository.UpdateUser(user);
        return data;
    }
    public async Task UpdateBio(Guid userId, string bio)
    {
        var user = await unitOfWork.UserRepository.GetUserByid(userId);
        user.Bio = bio;
        await unitOfWork.UserRepository.UpdateUser(user);
    }

    public async Task<UserDto> UpdateUserGeneralInfo(Guid id, UpdateUserGeneralInfo generalInfo)
    {
        var user = await unitOfWork.UserRepository.GetUserByid(id);
        bool check = false;
        if (!string.IsNullOrEmpty(generalInfo.LastName))
        {
            user.LastName = generalInfo.LastName;
            check = true;
        }
        if (!string.IsNullOrEmpty(generalInfo.FirsName))
        {
            user.FirsName = generalInfo.FirsName;
            check = true;
        }
        if (!string.IsNullOrEmpty(generalInfo.Age))
        {
            byte age;
            try
            {
                age = byte.Parse(generalInfo.Age);
                user.Age = age;
                check = true;
            }
            catch (Exception e)
            {
                throw new Exception("Age must be number");
            }
        }
        if(check) await unitOfWork.UserRepository.UpdateUser(user);
        return user.ParseUserToDto();
    }

    public async Task<UserDto> UpdateUsername(Guid id, UpdateUsernameModel model)
    {
        
    }

    private async Task CheckForExist(string username)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsername(username)!;
        if (user is not null)
            throw new UserExistException();
    }
    private string GetGender(string gender)
    {
        return gender.ToUpper() == UserConstants.Famele ? gender : UserConstants.Male;
    }
}

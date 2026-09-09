using Chat.Api.Constants;
using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Extentions;
using Chat.Api.Helpers;
using Chat.Api.Models;
using Chat.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Chat.Api.Managers;

public class UserManager(IUnitOfWork unitOfWork, JwtManager jwtManager)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly JwtManager _jwtmanager = jwtManager;
    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAllUsers();
        return users.ParseUserDtos();
    }
    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetUserByid(id);
        return user.ParseUserToDto();
    }
    public async Task<UserDto> GetUserByUsername(string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsername(username)!;
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
        await _unitOfWork.UserRepository.AddUser(user);
        return user.ParseUserToDto();

    }
    public async Task<string> Login(LoginModel model)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsername(model.Username)!;
        if (user is null)
            throw new Exception("Username is invalid");
        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswrodHash, model.Passwor);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid password");

        var token = _jwtmanager.GenerateToken(user);
        return token; 
    }
    public async Task<byte[]> AddOrUpdatePhoto(Guid userId, IFormFile file)
    {
        var user = await _unitOfWork.UserRepository.GetUserByid(userId);

        StaticHelper.IsPhoto(file);
        var data = StaticHelper.PhotoFileToArray(file);

        user.ProfilePhotoData = data;
        await _unitOfWork.UserRepository.UpdateUserById(user);
        return data;
    }
    private async Task CheckForExist(string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsername(username)!;
        if (user is not null)
            throw new UserExistException();
    }
    private string GetGender(string gender)
    {
        return gender.ToUpper() == UserConstants.Famele ? gender : UserConstants.Male;
    }
}

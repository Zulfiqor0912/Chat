using Chat.Api.Constants;
using Chat.Api.Exceptions;
using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models.UserModels;
using Chat.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    UserManager userManager,
    UserHelper userHelper) : ControllerBase
{
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userManager.GetAllUsers();
        return Ok(users);
    }
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserById()
    {
        try
        {
            var id = userHelper.GetUserId();
            var user = await userManager.GetUserById(id);
            return Ok(user);
        }
        catch (UserNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel model)
    {
        var result = await userManager.Register(model);
        return Ok();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var result = await userManager.Login(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpPut("/add-or-update-photo")]
    public async Task<IActionResult> AddOrUpdateUserPhoto([FromForm] FileClass fileClass)
    {
        var userId = userHelper.GetUserId();
        var result = await userManager.AddOrUpdatePhoto(userId, fileClass.File!);
        return Ok(result);
    }
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpPost("/update-bio")]
    public async Task<IActionResult> UpdateBio([FromBody] string bio)
    {
        var userId = userHelper.GetUserId();
        await userManager.UpdateBio(userId, bio);
        return Ok();
    }
    
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpPost("/update-user-general-info")]
    public async Task<IActionResult> UpdateUserGeneralInfo([FromBody] UpdateUserGeneralInfo info)
    {
        try
        {
            var id = userHelper.GetUserId();
            var result = await userManager.UpdateUserGeneralInfo(id, info);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message); 
        }
    }
    
    [Authorize(Roles = $"{UserConstants.Admin},{UserConstants.User}")]
    [HttpPost("/update-username")]
    public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameModel model)
    {
        try
        {
            var id = userHelper.GetUserId();
            var result = await userManager.UpdateUsername(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

public class FileClass
{
    public IFormFile? File { get; set; }
}

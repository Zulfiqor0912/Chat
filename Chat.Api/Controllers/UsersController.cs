using Chat.Api.Exceptions;
using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models;
using Chat.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserManager userManager,
    UserHelper userHelper) : ControllerBase
{
    private readonly UserManager _userManager = userManager;
    private readonly UserHelper _userHelper = userHelper;

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.GetAllUsers();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserById()
    {
        try
        {
            var id = _userHelper.GetUserId();
            var user = await _userManager.GetUserById(id);
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
        var result = await _userManager.Register(model);
        return Ok();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var result = await _userManager.Login(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("{userId:guid}/add-or-update-photo")]
    public async Task<IActionResult> AddOrUpdateUserPhoto(Guid userId, [FromForm] FileClass fileClass)
    {
        var result = await _userManager.AddOrUpdatePhoto(userId, fileClass.File!);
        return Ok(result);
    }
}

public class FileClass
{
    public IFormFile? File { get; set; }
}

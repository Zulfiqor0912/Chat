using Chat.Api.Entities;
using Chat.Api.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirsName { get; set; }
    public string? LastName { get; set; }
    public string Username { get; set; }
    public byte Age { get; set; }
    public string Gender { get; set; }
    public string ProfilePhoto { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; } 
    public UserStatus Status { get; set; } = UserStatus.Active;
    public List<UserChat>? UserChats { get; set; }
}

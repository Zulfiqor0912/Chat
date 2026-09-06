using Chat.Api.Entities;
using Chat.Api.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class UpdateUserModel
{
    public string? FirsName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public byte Age { get; set; }
    public string? Gender { get; set; }
    public byte[]? ProfilePhotoData { get; set; }
    public string? Bio { get; set; }
    public UserStatus Status { get; set; }
}

using Chat.Api.Entities;
using Chat.Api.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models.UserModels;

public class UpdateUserGeneralInfo
{
    public string? FirsName { get; set; }
    public string? LastName { get; set; }
    // public string? Username { get; set; }
    public string? Age { get; set; }
    // public string? Gender { get; set; }
    // public byte[]? ProfilePhotoData { get; set; }
    // public string? Bio { get; set; }
    // public UserStatus Status { get; set; }
}

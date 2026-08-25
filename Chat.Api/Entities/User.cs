using Chat.Api.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class User
{
    public Guid Id { get; set; }
    [Required]
    public string FirsName { get; set; } = null!;
    public string? LastName { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string PasswrodHash { get; set; } = null!;
    public byte Age { get; set; }
    [Required]
    public string Gender { get; set; } = null!;
    public string ProfilePhoto { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public List<UserChat>? UserChats { get; set; }
}

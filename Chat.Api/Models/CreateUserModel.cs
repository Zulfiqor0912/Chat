using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class CreateUserModel
{
    [Required]
    public string FirsName { get; set; } = null!;
    public string? LastName { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    public byte Age { get; set; }
    [Required]
    public string Gender { get; set; } = null!;
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

}

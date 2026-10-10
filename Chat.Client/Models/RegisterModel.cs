using System.ComponentModel.DataAnnotations;

namespace Chat.Client.Models.UserModels;

public class RegisterModel
{
    [Required]
    public string FirsName { get; set; } = null!;
    public string? LastName { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    public byte Age { get; set; }
    public string Gender { get; set; } = "MALE";
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

}

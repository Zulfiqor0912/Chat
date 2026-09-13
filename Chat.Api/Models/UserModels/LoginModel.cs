using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models.UserModels;

public class LoginModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Passwor { get; set; }
}

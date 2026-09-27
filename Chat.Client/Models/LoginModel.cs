using System.ComponentModel.DataAnnotations;

namespace Chat.Client.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Passwor { get; set; }
}
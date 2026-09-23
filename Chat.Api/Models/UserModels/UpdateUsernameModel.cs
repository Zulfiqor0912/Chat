using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models.UserModels;

public class UpdateUsernameModel
{
    [Required] 
    public string Username { get; set; } = null!;
}
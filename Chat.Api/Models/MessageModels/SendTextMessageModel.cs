using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models.MessageModels;

public class SendTextMessageModel
{
    [Required]
    public string Text { get; set; } = null!;
}

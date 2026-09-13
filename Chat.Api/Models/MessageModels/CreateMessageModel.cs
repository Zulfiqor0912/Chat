using Chat.Api.Entities;
using Chat.Api.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models.MessageModels;

public class CreateMessageModel
{
    public int Id { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public Guid FromUserId { get; set; }
    public bool IsEdited { get; set; }
    [Required]
    public string FromUserName { get; set; } = null!;
    public MessageStatus Status { get; set; } = MessageStatus.Active;
    public Content? Content { get; set; }
    public Guid ChatId { get; set; }
    public DateTime SendAt => DateTime.UtcNow;
    public DateTime EditedAt { get; set; }
}

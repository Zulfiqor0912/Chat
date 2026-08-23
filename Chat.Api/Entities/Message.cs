using Chat.Api.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public Guid FromUserId { get; set; }
    public bool IsEdited { get; set; }
    [Required]
    public string FromUserName { get; set; } = null!;
    public MessageStatus Status { get; set; } = MessageStatus.Active;
    public int ContentId { get; set; }
    public Content? Content { get; set; }
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;
    public DateTime SendAt => DateTime.UtcNow; 
    public DateTime EditedAt { get; set; }
}

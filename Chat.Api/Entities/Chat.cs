using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class Chat
{
    public Guid Id { get; set; }
    [Required]
    public List<string> ChatNames { get; set; } = null!;
    public List<Message> Messages { get; set; }
    public List<UserChat>? UserChats { get; set; }
}

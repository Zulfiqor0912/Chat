using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class UserChat
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid ChatId { get; set; }
    public Chat? Chat { get; set; }
}

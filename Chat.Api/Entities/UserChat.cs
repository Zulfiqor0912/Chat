using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class UserChat
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid FirstUserId { get; set; }
    [Required]
    public Guid LastUserId { get; set; }

    public Guid ChatId { get; set; }
    public User? FirstUser { get; set; }
    public User? LastUser { get; set; }
    public Chat? Chat { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Chat.Api.DTOs;

public class ContentDto
{
    public int Id { get; set; }
    [Required]
    public string Url { get; set; } = null!;
    public string? Type { get; set; }
    public int MessageId { get; set; }
}

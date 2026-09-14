using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class FileModel
{
    [Required]
    public IFormFile File { get; set; } = null!;
}

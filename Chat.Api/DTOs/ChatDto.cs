using Chat.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.DTOs;

public class ChatDto
{
    public Guid Id { get; set; }
    public List<string> ChatNames { get; set; } = null!;
    public List<MessageDto> MessageDtos { get; set; }
    public List<UserChatDto>? UserChatDtos { get; set; }
}

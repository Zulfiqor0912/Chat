using Chat.Api.Entities;

namespace Chat.Api.DTOs;

public class UserChatDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
}

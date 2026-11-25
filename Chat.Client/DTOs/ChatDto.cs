namespace Chat.Client.DTOs;

public class ChatDto
{
    public Guid Id { get; set; }
    public List<string> ChatNames { get; set; } = null!;
    public List<MessageDto> Messages { get; set; }
    public List<UserChatDto>? UserChats { get; set; }
}
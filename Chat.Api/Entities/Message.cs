namespace Chat.Api.Entities;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public Guid FromUserId { get; set; }
    public Guid ChatId { get; set; }
    public Content Content { get; set; }
    public Chat Chat { get; set; }
}

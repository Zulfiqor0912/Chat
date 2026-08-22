namespace Chat.Api.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirsName { get; set; }
    public string? LastName { get; set; }
    public string Username { get; set; }
    public string PasswrodHash { get; set; }
    public byte Age { get; set; }
    public string Gender { get; set; }
    public string ProfilePhoto { get; set; }
    public string Bio { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public Enum Status { get; set; }
    public List<UserChat> UserChats { get; set; }
}

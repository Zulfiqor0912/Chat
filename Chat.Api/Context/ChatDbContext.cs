using Chat.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Context;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {

    }


    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    //public DbSet<Content> Contents { get; set; }
    public DbSet<Entities.Chat> Chats { get; set; }
    public DbSet<UserChat> UserChats { get; set; }
    
}

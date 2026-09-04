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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserChat>(entity =>
        {
            entity.HasKey(uc => uc.Id);

            entity.Property(uc => uc.FirstUserId).IsRequired();
            entity.Property(uc => uc.LastUserId).IsRequired();
            entity.Property(uc => uc.ChatId).IsRequired();

            entity.HasOne(uc => uc.FirstUser)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.FirstUserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(uc => uc.Chat)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasOne(m => m.Chat)
                .WithMany(ch => ch.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
}

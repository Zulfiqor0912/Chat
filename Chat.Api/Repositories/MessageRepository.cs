using Chat.Api.Context;
using Chat.Api.Entities;
using Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories;

public class MessageRepository(ChatDbContext dbContext) : IMessageRepository
{
    private readonly ChatDbContext _dbContext = dbContext;
    public async Task Addmessage(Message message)
    {
        await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Message>> GetMessage()
    {
        var messages = await _dbContext.Messages.ToListAsync();
        return messages;
    }

    public async Task<List<Message>> GetMessageByChatId(Guid chatId)
    {
        var messages = await _dbContext.Messages
            .Where(m => m.ChatId == chatId)
            .ToListAsync();
        return messages;
    }

    public async Task<Message> GetMessageById(int id)
    {
        var message = await _dbContext.Messages
            .SingleOrDefaultAsync(m => m.Id == id);
        return message!;
    }

    public async Task<Message> GetMessageById(Guid chatId, int id)
    {
        var message = await _dbContext.Messages
            .FirstOrDefaultAsync(m => m.ChatId == chatId && m.Id == id);
        return message!;
    }
}

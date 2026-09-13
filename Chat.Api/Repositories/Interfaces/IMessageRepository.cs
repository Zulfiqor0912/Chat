using Chat.Api.Entities;

namespace Chat.Api.Repositories.Interfaces;

public interface IMessageRepository
{
    public Task<Message> GetMessageById(int id); //only for admin
    public Task<List<Message>> GetMessage();//only for admin 
    public Task<List<Message>> GetMessageByChatId(Guid chatId);
    public Task<Message> GetMessageById(Guid chatId, int id);
    public Task Addmessage(Message message);
}

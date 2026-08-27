using Chat.Api.Entities;

namespace Chat.Api.Repositories.Interfaces;

public interface IUserChatRepository
{
    public Task AddUserChat(UserChat userChat);
    public Task DeleteUserChat(UserChat userChat);
}

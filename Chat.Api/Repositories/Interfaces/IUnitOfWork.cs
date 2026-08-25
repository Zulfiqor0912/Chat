namespace Chat.Api.Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IChatRepository ChatRepository { get; }
}

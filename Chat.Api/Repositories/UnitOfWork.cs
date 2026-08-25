using Chat.Api.Context;
using Chat.Api.Repositories.Interfaces;

namespace Chat.Api.Repositories;

public class UnitOfWork(ChatDbContext dbContext) : IUnitOfWork
{
    public IUserRepository userRepository { get; }
    public IChatRepository chatRepository { get; }

    public IUserRepository UserRepository => userRepository ?? new UserRepository(dbContext);

    public IChatRepository ChatRepository => chatRepository ?? new ChatRepository(dbContext);
    //{
    //    get
    //    {
    //        if (userRepository == null)
    //            return new UserRepository(dbContext);
    //        return userRepository;
    //    }
    //}



}

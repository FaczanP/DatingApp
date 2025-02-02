using API.Data;

namespace API.Interfaces;

public interface IUnitOfWork
{
    UserRepository UserRepository { get; }
    MessageRepository MessageRepository{ get; }
    LikesRepository LikesRepository{ get; }
    Task<bool> Complete();
    bool HasChanges();
}

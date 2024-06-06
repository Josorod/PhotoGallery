using WebApi.Data.Entities;

namespace WebApi.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IGenericRepository<Album> Albums { get; }
        IGenericRepository<Like> Likes { get; }
        IGenericRepository<Photo> Photos { get; }
        Task<bool> SaveAsync();
    }
}

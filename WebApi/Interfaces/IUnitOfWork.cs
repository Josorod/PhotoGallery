namespace WebApi.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task<bool> SaveAsync();
    }
}

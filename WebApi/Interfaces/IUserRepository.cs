using WebApi.Models;


namespace WebApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string email, string password);
        void Register(string email, string password);
        Task<bool> UserAlreadyExists(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByUserNameAsync(string email);
        Task<User> GetByIdAsync(int id);

    }
}

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;
        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            return await dc.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public void Register(string email, string password)
        {

            User user = new User();
            user.Email = email;
            user.Password = password;

            dc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string email)
        {
            return await dc.Users.AnyAsync(x => x.Email == email);
        }
    }
}

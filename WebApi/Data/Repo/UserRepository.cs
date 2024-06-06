using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dc.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await dc.Users.FindAsync(id);
        }


        public async Task<User> GetByUserNameAsync(string email)
        {
            return await dc.Users.SingleOrDefaultAsync(u => u.Email == email);
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

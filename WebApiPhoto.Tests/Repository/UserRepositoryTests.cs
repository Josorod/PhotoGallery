using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Repo;
using WebApi.Models;

namespace WebApiPhoto.Tests.Repository
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly DataContext _context;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            _context = new DataContext(_options);
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void Register_AddsUserToDatabase()
        {
            // Arrange
            string email = "user3@example.com";
            string password = "password3";

            // Act
            _repository.Register(email, password);
            _context.SaveChanges();

            // Assert
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            // Arrange
            _context.Users.Add(new User { Email = "user1@example.com", Password = "password1" });
            _context.Users.Add(new User { Email = "user2@example.com", Password = "password2" });
            _context.SaveChanges();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            var users = Assert.IsType<List<User>>(result);
            Assert.Equal(2, users.Count);
        }
    }
}

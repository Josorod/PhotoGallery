using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApiPhoto.Tests.Controller
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public UserControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockUnitOfWork.Setup(u => u.UserRepository).Returns(_mockUserRepository.Object);

            _controller = new UserController(_mockUnitOfWork.Object,_mockConfiguration.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var userDTOs = new List<User> { new User { Id = 1, Email = "Test User" } };
            var users = new List<User> { new User { Id = 1, Email = "Test User" } };

            _mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(userDTOs);
            _mockMapper.Setup(m => m.Map<IEnumerable<User>>(It.IsAny<IEnumerable<User>>())).Returns(users);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<User>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Test User", returnValue[0].Email);
        }

        [Fact]
        public async Task GetUser_ReturnsOkResult_WithUser()
        {
            // Arrange
            var userId = 1;
            var userDTO = new User { Id = userId, Email = "Test User" };
            var user = new User { Id = userId, Email = "Test User" };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(userDTO);
            _mockMapper.Setup(m => m.Map<User>(It.IsAny<User>())).Returns(user);

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(userId, returnValue.Id);
            Assert.Equal("Test User", returnValue.Email);
        }


    }
}

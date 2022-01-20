using AutoFixture;
using CarMarket.BusinessLogic.Tests.Helpers;
using CarMarket.BusinessLogic.User.Service;
using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Exceptions;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CarMarket.BusinessLogic.Tests
{
    public class UserServiceTests
    {
        // Some test methods fails cause UserRepository is mock

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<UserManager<UserModel>> _userManagerMock;
        private readonly IUserService _userService;

        private List<UserModel> _usersMock = new()
        {
            new Fixture().Create<UserModel>(),
            new Fixture().Create<UserModel>()
        };

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userManagerMock = MockHelper.MockUserManager(_usersMock);
            _userService = new UserService(_userRepositoryMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async void Get_CarIdIsValid_ShouldReturnUser()
        {
            // Arrange
            var user = CreateAdminUser();

            // Act
            var result = await _userService.GetAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(x => x.FindByIdAsync(user.Id), Times.Once);
        }

        [Fact]
        public async void Get_ShouldReturnAllUsers()
        {
            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(x => x.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async void Get_UserEmailIsValid_ShouldReturnUser()
        {
            // Arrange
            var user = CreateAdminUser();

            // Act
            var result = await _userService.GetByEmailAsync(user.Email);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(x => x.FindByEmailAsync(user.Email), Times.Once);
        }

        [Fact]
        public async void Get_ShouldReturnUsersByPage()
        {
            // Arrange
            var random = new Random();

            var pageNumber = random.Next(1, 5);
            var pageSize = random.Next(1, 5);

            // Act
            var result = await _userService.GetByPageAsync(pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(x => x.FindByPageAsync(pageNumber, pageSize), Times.Once);
        }

        [Fact]
        public async void Get_UserRoleNameIsValid_ShouldReturnUsersInRole()
        {
            // Arrange
            var roleName = "Admin";

            // Act
            var result = await _userService.GetRoleAsync(roleName);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(x => x.FindRoleAsync(roleName), Times.Once);
        }

        [Fact]
        public async void Create_UserIsValid_ShouldCreateNewUser()
        {
            // Arrange
            var user = new Fixture().Create<UserModel>();

            // Act
            var result = await _userService.CreateAsync(user);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(x => x.SaveAsync(user), Times.Once);
        }

        [Fact]
        public void Create_UserIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            UserModel user = null;

            // Act
            string userId = null;

            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                userId = await _userService.CreateAsync(user);
            });

            // Assert
            _userRepositoryMock.Verify(x => x.SaveAsync(user), Times.Never);
        }

        [Fact]
        public void Delete_CarIsInvalid_ShouldThrowsCarNotFoundException()
        {
            // Arrange
            var userId = new Fixture().Create<string>();

            // Act
            var exception = Assert.ThrowsAsync<CarNotFoundException>(async () =>
            {
                await _userService.DeleteAsync(userId);
            });

            // Assert 
            _userRepositoryMock.Verify(x => x.DeleteAsync(userId), Times.Never);
        }

        [Fact]
        public void Update_UserIsInvalid_ShouldThrowsUserNotFoundException()
        {
            // Arrange
            var userId = new Fixture().Create<string>();
            var user = new Fixture().Create<UserModel>();

            // Act
            var exception = Assert.ThrowsAsync<CarNotFoundException>(async () =>
            {
                await _userService.UpdateUserAsync(userId, user);
            });

            // Assert 
            _userRepositoryMock.Verify(x => x.UpdateAsync(userId, user), Times.Never);
        }

        private static UserModel CreateAdminUser()
        {
            return new UserModel()
            {
                Id = "qwe",
                Email = "admin@gmail.com"
            };
        }
    }
}

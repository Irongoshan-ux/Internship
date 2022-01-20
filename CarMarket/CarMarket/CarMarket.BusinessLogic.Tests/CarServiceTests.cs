using AutoFixture;
using CarMarket.BusinessLogic.Car.Service;
using CarMarket.BusinessLogic.Tests.Helpers;
using CarMarket.BusinessLogic.User.Service;
using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Exceptions;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CarMarket.BusinessLogic.Tests
{
    public class CarServiceTests
    {
        // Some test methods fails cause CarRepository is mock

        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<UserService> _userServiceMock;
        private readonly Mock<UserManager<UserModel>> _userManagerMock;
        private readonly CarService _carService;

        private List<UserModel> _users = new()
        {
            new Fixture().Create<UserModel>(),
            new Fixture().Create<UserModel>()
        };


        public CarServiceTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userManagerMock = MockHelper.MockUserManager(_users);
            _userServiceMock = new Mock<UserService>(_userRepositoryMock.Object, _userManagerMock.Object);
            _carService = new CarService(_carRepositoryMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public async void Get_CarIdIsValid_ShouldReturnCar()
        {
            // Arrange
            var carId = 1L;

            // Act
            var result = await _carService.GetAsync(carId);

            // Assert
            Assert.NotNull(result);
            _carRepositoryMock.Verify(x => x.FindByIdAsync(carId), Times.Once);
        }

        [Fact]
        public async void Get_ShouldReturnAllCars()
        {
            // Act
            var result = await _carService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            _carRepositoryMock.Verify(x => x.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async void Get_CarOwnerIsValid_ShouldReturnAllUserCars()
        {
            // Arrange
            var carOwner = CreateAdminUser();

            // Act
            var result = await _carService.GetAllUserCarsAsync(carOwner.Id);

            // Assert
            Assert.NotNull(result);
            _carRepositoryMock.Verify(x => x.FindAllUserCarsAsync(carOwner.Id), Times.Once);
        }

        [Fact]
        public async void Get_CarOwnerIsValid_ShouldReturnCarsByPage()
        {
            // Arrange
            var random = new Random();

            var pageNumber = random.Next(1, 5);
            var pageSize = random.Next(1, 5);

            // Act
            var result = await _carService.GetByPageAsync(pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            _carRepositoryMock.Verify(x => x.FindByPageAsync(pageNumber, pageSize), Times.Once);
        }

        [Fact]
        public async void Search_CarNameIsValid_ShouldReturnCarsWithCarName()
        {
            // Arrange
            var carName = "test";
            CarType carType = CarType.Convertible;
            // Act
            var result = await _carService.SearchAsync(carName, carType);

            // Assert
            Assert.NotNull(result);
            _carRepositoryMock.Verify(x => x.SearchAsync(carName, carType), Times.Once);
        }

        [Fact]
        public async void Create_CarIsValid_ShouldCreateNewCar()
        {
            // Arrange
            var car = new Fixture().Create<CarModel>();
            car.Owner = CreateAdminUser();

            // Act
            var result = await _carService.CreateAsync(car);

            // Assert
            Assert.NotNull(result);
            _carRepositoryMock.Verify(x => x.AddAsync(car), Times.Once);
        }

        [Fact]
        public void Create_CarIsNull_ShouldThrowNullReferenceException()
        {
            // Arrange
            CarModel car = null;

            // Act
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                car = await _carService.CreateAsync(car);
            });

            // Assert
            _carRepositoryMock.Verify(x => x.AddAsync(car), Times.Never);
        }

        [Fact]
        public void Delete_CarIsInvalid_ShouldThrowsCarNotFoundException()
        {
            // Arrange
            var carId = new Fixture().Create<long>();

            // Act
            var exception = Assert.ThrowsAsync<CarNotFoundException>(async () =>
            {
                await _carService.DeleteAsync(carId);
            });

            // Assert 
            _carRepositoryMock.Verify(x => x.DeleteAsync(carId), Times.Never);
        }

        [Fact]
        public void Update_CarIsInvalid_ShouldThrowsCarNotFoundException()
        {
            // Arrange
            var carId = new Fixture().Create<long>();
            var car = new Fixture().Create<CarModel>();

            // Act
            CarModel updatedCar = null;

            var exception = Assert.ThrowsAsync<CarNotFoundException>(async () =>
            {
                updatedCar = await _carService.UpdateCarAsync(carId, car);
            });

            // Assert 
            Assert.Null(updatedCar);
            _carRepositoryMock.Verify(x => x.UpdateAsync(carId, car), Times.Never);
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

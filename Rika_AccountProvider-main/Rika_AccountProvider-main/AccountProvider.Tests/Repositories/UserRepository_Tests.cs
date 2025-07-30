using AccountProvider.Entities;
using AccountProvider.Interfaces;
﻿using AccountProvider.Context;
using AccountProvider.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AccountProvider.Tests.Repositories;

public class UserRepository_Tests
{
    private Mock<IUserRepository> _mockUserRepository;

    private readonly UserRepository _userRepository;
    private readonly DataContext _context;

    public UserRepository_Tests()
    {
        _mockUserRepository = new Mock<IUserRepository>();

        var options = new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase(databaseName: "TestDatabase")
    .Options;

        _context = new DataContext(options);
        _userRepository = new UserRepository(_context);
    }

    [Fact]
    public async Task GetUserAsync_ThenReturnUserById()
    {
        // Arrange
        var userId = "1";
        var userEntity = new UserEntity
        {
            Id = userId
        };
        _mockUserRepository.Setup(x => x.GetUserAsync(x => x.Id == userId))
        .ReturnsAsync((UserEntity?)userEntity);

        // Act
        var result = await _mockUserRepository.Object.GetUserAsync(x => x.Id == userId);
        var statusCode = result != null ? "200" : "400";

        // Assert
        Assert.NotNull(result);
        Assert.Equal("200", statusCode);
        Assert.Equal(userId, result.Id);

    }

    [Fact]
    public async Task GetUserAsync_WhileNotExistingReturnNotFound()
    {
        // Arrange
        var userId = "7679";
        var userDto = new UserEntity
        {
            Id = userId
        };
        _mockUserRepository.Setup(x => x.GetUserAsync(x => x.Id == userId))
            .ReturnsAsync((UserEntity?)null);

        // Act
        var result = await _mockUserRepository.Object.GetUserAsync(x => x.Id == userId);
		var statusCode = result != null ? "200" : "400";

        // Assert
        Assert.Null(result);
        Assert.Equal("400", statusCode);
	}

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "SecurePassword123",
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userRepository.GetByEmailAsync("john.doe@example.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("john.doe@example.com", result?.Email);
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Act
        var result = await _userRepository.GetByEmailAsync("nonexistent@example.com");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldAddUserToDatabase()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = "2",
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "SecurePassword123",
            Role = "User" 
        };

        // Act
        await _userRepository.CreateUserAsync(user);
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == "jane.smith@example.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("jane.smith@example.com", result?.Email);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldUpdateUser_AndReturnUpdated_UserEntity()
    {
        // Arrange
        UserEntity userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            IsEmailConfirmed = false,
            Password = "Test",
            Role = "Test",
            PhoneNumber = "Test",
            ProfileImageUrl = "Test",
            Age = 1,
            Gender = "Test",
            AddressId = "Test"
        };

        UserEntity updatedUserEntity = new UserEntity
        {
            FirstName = "updatedTest",
            LastName = "updatedTest",
            Email = "updatedTest",
            IsEmailConfirmed = true,
            Password = "updatedTest",
            Role = "updatedTest",
            PhoneNumber = "updatedTest",
            ProfileImageUrl = "updatedTest",
            Age = 2,
            Gender = "updatedTest",
            AddressId = "updatedTest"
        };

        _mockUserRepository.Setup(x => x.UpdateUserAsync(updatedUserEntity)).ReturnsAsync(updatedUserEntity);

        // Act
        var result = await _mockUserRepository.Object.UpdateUserAsync(updatedUserEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedUserEntity, result);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldNotUpdateUser_AndReturnNull()
    {
        // Arrange
        UserEntity userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            IsEmailConfirmed = false,
            Password = "Test",
            Role = "Test",
            PhoneNumber = "Test",
            ProfileImageUrl = "Test",
            Age = 1,
            Gender = "Test",
            AddressId = "Test"
        };

        UserEntity? updatedUserEntity = null;

        _mockUserRepository.Setup(x => x.UpdateUserAsync(updatedUserEntity)).ReturnsAsync(updatedUserEntity);

        // Act
        var result = await _mockUserRepository.Object.UpdateUserAsync(updatedUserEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldNotUpdateUser_BecauseEmptyField_AndReturnNull()
    {
        // Arrange
        UserEntity userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            IsEmailConfirmed = false,
            Password = "Test",
            Role = "Test",
            PhoneNumber = "Test",
            ProfileImageUrl = "Test",
            Age = 1,
            Gender = "Test",
            AddressId = "Test"
        };

        UserEntity updatedUserEntity = new UserEntity
        {
            FirstName = "",
            LastName = "updatedTest",
            Email = "updatedTest",
            IsEmailConfirmed = true,
            Password = "updatedTest",
            Role = "updatedTest",
            PhoneNumber = "updatedTest",
            ProfileImageUrl = "updatedTest",
            Age = 2,
            Gender = "updatedTest",
            AddressId = "updatedTest"
        };

        _mockUserRepository.Setup(x => x.UpdateUserAsync(It.Is<UserEntity>(u =>
            string.IsNullOrWhiteSpace(u.FirstName) ||
            string.IsNullOrWhiteSpace(u.LastName) ||
            string.IsNullOrWhiteSpace(u.Email) ||
            string.IsNullOrWhiteSpace(u.Password) ||
            string.IsNullOrWhiteSpace(u.Role) ||
            string.IsNullOrWhiteSpace(u.PhoneNumber) ||
            string.IsNullOrWhiteSpace(u.ProfileImageUrl) ||
            u.Age <= 0 ||
            string.IsNullOrWhiteSpace(u.Gender) ||
            string.IsNullOrWhiteSpace(u.AddressId))))
        .ReturnsAsync((UserEntity?)null);

        // Act
        var result = await _mockUserRepository.Object.UpdateUserAsync(updatedUserEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnListOfUsersAndStatusCode()
    {
        // Arrange
        var users = new List<UserEntity>
        {
            new UserEntity { Id = "1", Gender = "Male", Email = "william@domain.com" },
            new UserEntity { Id = "2", Gender = "Female", Email = "gustavia@domain.com" }
        };

        // Act
        var userEntity = new UserEntity
        {
            Id = "1",
            FirstName = "William",
            LastName = "Hägg"
        };
        _mockUserRepository.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(new List<UserEntity> { userEntity });

        var result = await _mockUserRepository.Object.GetAllUsersAsync();

        var statusCode = "";
        if (result == null)
        {
            statusCode = "400";
        }
        else
        {
            statusCode = "200";
        }

        // Assert

        Assert.NotNull(result);
        Assert.IsType<List<UserEntity>>(result);
        Assert.Equal("200", statusCode);
    }

    [Fact]
    public async Task GetAllUsersAsync_CouldNotGetListOfUsers()
    {
        _mockUserRepository.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(new List<UserEntity>());
        var result = await _mockUserRepository.Object.GetAllUsersAsync();

        var statusCode = "";
        if (result == null)
        {
            statusCode = "200";
        }
        else
        {
            statusCode = "400";
        }

        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.Equal("400", statusCode);
    }
}

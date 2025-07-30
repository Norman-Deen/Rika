using AccountProvider.Factories;
using AccountProvider.Models;

namespace AccountProvider.Tests.Factories;

public class CreateUserFactory_Tests
{
    [Fact]
    public void CreateUserEntity_ShouldReturnUserEntity_WithCorrectProperties()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            ProfileImageUrl = "https://example.com/profile.jpg",
            Gender = "Male",
            Age = 30,
            Password = "SecurePassword123"
        };
        string passwordHash = "hashedPassword123";

        // Act
        var result = CreateUserFactory.CreateUserEntity(createUserDto, passwordHash);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createUserDto.FirstName, result.FirstName);
        Assert.Equal(createUserDto.LastName, result.LastName);
        Assert.Equal(createUserDto.Email, result.Email);
        Assert.Equal(passwordHash, result.Password);
        Assert.Equal("User", result.Role);
        Assert.False(result.IsEmailConfirmed);
        Assert.Equal(createUserDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(createUserDto.ProfileImageUrl, result.ProfileImageUrl);
        Assert.Equal(createUserDto.Gender, result.Gender);
        Assert.Equal(createUserDto.Age, result.Age);
    }

    [Fact]
    public void CreateUserEntity_ShouldHandleNullCreateUserDto_AndReturnNull()
    {
        // Act
        var result = CreateUserFactory.CreateUserEntity(null!, "hashedPassword123");

        // Assert
        Assert.Null(result);
    }
}

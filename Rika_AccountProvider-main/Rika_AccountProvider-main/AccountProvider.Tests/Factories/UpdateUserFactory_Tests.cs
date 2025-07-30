using AccountProvider.Entities;
using AccountProvider.Factories;
using AccountProvider.Models;

namespace AccountProvider.Tests.Factories;

public class UpdateUserFactory_Tests
{
	[Fact]
	public void UpdateUserEntity_ShouldUpdateFields_WhenDtoAndEntityAreValid()
	{
		// Arrange
		var updateUserDto = new UpdateUserDto
		{
			FirstName = "UpdatedTest",
			LastName = "UpdatedTest",
			Email = "UpdatedTest@Test.com",
			Password = "UpdatedTest",
			PhoneNumber = "UpdatedTest",
			ProfileImageUrl = "UpdatedTest",
			Age = 30,
			Gender = "UpdatedTest"
		};

		var userEntity = new UserEntity
		{
			FirstName = "Test",
			LastName = "Test",
			Email = "Test@example.com",
			Password = "Test",
			PhoneNumber = "Test",
			ProfileImageUrl = "Test",
			Age = 25,
			Gender = "Test"
		};

		// Act
		var result = UpdateUserFactory.UpdateUserEntity(updateUserDto, userEntity);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("UpdatedTest", result.FirstName);
		Assert.Equal("UpdatedTest", result.LastName);
		Assert.Equal("UpdatedTest@Test.com", result.Email);
		Assert.Equal("UpdatedTest", result.Password);
		Assert.Equal("UpdatedTest", result.PhoneNumber);
		Assert.Equal("UpdatedTest", result.ProfileImageUrl);
		Assert.Equal(30, result.Age);
		Assert.Equal("UpdatedTest", result.Gender);
	}

	[Fact]
	public void UpdateUserEntity_ShouldReturnNull_WhenDtoAndEntityAreNull()
	{
		// Arrange
		UpdateUserDto? updateUserDto = null;
		UserEntity? userEntity = null;

		// Act
		var result = UpdateUserFactory.UpdateUserEntity(updateUserDto, userEntity);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void UpdateUserDto_ShouldUpdateFields_WhenEntityIsValid()
	{
		// Arrange
		var userEntity = new UserEntity
		{
			FirstName = "Test",
			LastName = "Test",
			Email = "Test@example.com",
			Password = "Test",
			PhoneNumber = "Test",
			ProfileImageUrl = "Test",
			Age = 25,
			Gender = "Test"
		};

		// Act
		var result = UpdateUserFactory.UpdateUserDto(userEntity);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Test", result.FirstName);
		Assert.Equal("Test", result.LastName);
		Assert.Equal("Test@example.com", result.Email);
		Assert.Equal("Test", result.Password);
		Assert.Equal("Test", result.PhoneNumber);
		Assert.Equal("Test", result.ProfileImageUrl);
		Assert.Equal(25, result.Age);
		Assert.Equal("Test", result.Gender);
	}

	[Fact]
	public void UpdateUserDto_ShouldReturnNull_WhenEntityIsNull()
	{
		// Arrange
		UserEntity? userEntity = null;

		// Act
		var result = UpdateUserFactory.UpdateUserDto(userEntity);

		// Assert
		Assert.Null(result);
	}
}

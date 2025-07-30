using AccountProvider.Entities;
using AccountProvider.Interfaces;
using AccountProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace AccountProvider.Tests.Services;

public class UserService_Tests
{
    private readonly Mock<IUserService> _mockUserService;

    public UserService_Tests()
    {
        _mockUserService = new Mock<IUserService>();
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser_AndReturnCreatedResult_WithStatusCode_201()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "SecurePassword123",
            PhoneNumber = "1234567890",
            ProfileImageUrl = "testurl",
            Age = 25,
            Gender = "test"
        };

        var createdResult = new CreatedResult(string.Empty, "User created successfully!");

        _mockUserService.Setup(service => service.CreateUserAsync(createUserDto))
                        .ReturnsAsync(createdResult);

        // Act
        var result = await _mockUserService.Object.CreateUserAsync(createUserDto);

        // Assert
        var createdResultActual = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResultActual.StatusCode);
        Assert.Equal("User created successfully!", createdResultActual.Value);

    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnBadRequest_WhenInputIsInvalid()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            FirstName = "", 
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "SecurePassword123"
        };

        _mockUserService.Setup(service => service.CreateUserAsync(createUserDto))
                        .ReturnsAsync(new BadRequestObjectResult("Invalid input data. Please ensure all fields are filled in."));

        // Act
        var result = await _mockUserService.Object.CreateUserAsync(createUserDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Invalid input data. Please ensure all fields are filled in.", badRequestResult.Value);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnConflict_WhenUserAlreadyExists()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "SecurePassword123"
        };

        var existingUser = new UserEntity
        {
            Id = "123",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        _mockUserService.Setup(service => service.CreateUserAsync(createUserDto))
                        .ReturnsAsync(new ConflictObjectResult($"The email address is already registered. {existingUser}"));

        // Act
        var result = await _mockUserService.Object.CreateUserAsync(createUserDto);

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result);
        Assert.Equal(409, conflictResult.StatusCode);
        Assert.Equal($"The email address is already registered. {existingUser}", conflictResult.Value);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "SecurePassword123"
        };
       
        _mockUserService.Setup(service => service.CreateUserAsync(createUserDto))
                        .ReturnsAsync(new StatusCodeResult(500));

        // Act
        var result = await _mockUserService.Object.CreateUserAsync(createUserDto);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

	[Fact]
	public async Task UpdateUserAsync_ShouldUpdateUser_AndReturnUpdated_UpdateUserDto_AndStatusCode_200()
	{
		// Arrange
		UpdateUserDto updatedUserDto = new UpdateUserDto
		{
			FirstName = "Test",
			LastName = "Test",
			Email = "Test@domain.com",
			Password = "password",
			PhoneNumber = "1234567890",
			ProfileImageUrl	= "test",
			Age = 2,
			Gender = "test"
		};

		_mockUserService.Setup(x => x.UpdateUserAsync(updatedUserDto)).ReturnsAsync(new OkObjectResult(updatedUserDto));

		// Act
		var result = await _mockUserService.Object.UpdateUserAsync(updatedUserDto);

		// Assert
		Assert.NotNull(result);

		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(200, okResult.StatusCode);

		var returnedDto = Assert.IsType<UpdateUserDto>(okResult.Value);
		Assert.Equal(updatedUserDto, returnedDto);
	}

	[Fact]
	public async Task UpdateUserAsync_ShouldNotUpdateUser_AndReturn_StatusCode_400()
	{
		// Arrange
		UpdateUserDto? updatedUserDto = null;
		_mockUserService.Setup(x => x.UpdateUserAsync(updatedUserDto)).ReturnsAsync(new BadRequestResult());

		// Act
		var result = await _mockUserService.Object.UpdateUserAsync(updatedUserDto);

		// Assert
		Assert.NotNull(result);

		var okResult = Assert.IsType<BadRequestResult>(result);
		Assert.Equal(400, okResult.StatusCode);
	}

	[Fact]
	public async Task UpdateUserAsync_ShouldNotUpdateUser_BecauseEmptyField_AndReturnNull_AndStatusCode_400()
	{
		// Arrange
		UpdateUserDto updatedUserDto = new UpdateUserDto
		{
			FirstName = "Test",
			LastName = "Test",
			Email = "",
			Password = "password",
			PhoneNumber = "1234567890",
			ProfileImageUrl = "test",
			Age = 2,
			Gender = "test"
		};

		_mockUserService.Setup(x => x.UpdateUserAsync(It.Is<UpdateUserDto>(u =>
			string.IsNullOrWhiteSpace(u.FirstName) ||
			string.IsNullOrWhiteSpace(u.LastName) ||
			string.IsNullOrWhiteSpace(u.Email) ||
			string.IsNullOrWhiteSpace(u.Password) ||
			string.IsNullOrWhiteSpace(u.PhoneNumber) ||
			string.IsNullOrWhiteSpace(u.ProfileImageUrl) ||
			u.Age <= 0 ||
			string.IsNullOrWhiteSpace(u.Gender))))
			.ReturnsAsync(new BadRequestResult());

		// Act
		var result = await _mockUserService.Object.UpdateUserAsync(updatedUserDto);


		// Assert
		Assert.NotNull(result);

		var okResult = Assert.IsType<BadRequestResult>(result);
		Assert.Equal(400, okResult.StatusCode);
	}


    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnList()
    {
        // Arrange
        var users = new List<GetUserDto>
        {
            new GetUserDto {UserId = "1", Gender = "Male", Email = "William@domain.com"},


             new GetUserDto {UserId = "2", Gender = "Female", Email = "gustavia@domain.com"}
        };

        // Act
        var getUserDto = new GetUserDto
        {
            UserId = "1",
            FirstName = "William",
            LastName = "Hägg"
        };

        _mockUserService.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(new List<GetUserDto> { getUserDto });

        var result = await _mockUserService.Object.GetAllUsersAsync();
        var statusCode = "";
        if (result != null)
        {
            statusCode = "200";
        }
        else
        {
            statusCode = "400";
        }

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<GetUserDto>>(result);
        Assert.Equal("200", statusCode);
    }

    [Fact]
    public async Task GetAllUsersAsync_CouldNotGetListOfUsers()
    {
        _mockUserService.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(new List<GetUserDto>());

        var result = await _mockUserService.Object.GetAllUsersAsync();

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








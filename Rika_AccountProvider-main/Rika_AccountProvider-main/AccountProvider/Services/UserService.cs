using AccountProvider.Context;
using AccountProvider.Entities;
using AccountProvider.Factories;
using AccountProvider.Interfaces;
using AccountProvider.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using System.Linq.Expressions;

namespace AccountProvider.Services;

public class UserService(DataContext context, IUserRepository userRepository) : IUserService
{
    private readonly DataContext _context = context;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<IActionResult> CreateUserAsync(CreateUserDto createUserDto)
    {
        try
        {
            if (string.IsNullOrEmpty(createUserDto.FirstName) ||
                string.IsNullOrEmpty(createUserDto.LastName) ||
                string.IsNullOrEmpty(createUserDto.Email) ||
                string.IsNullOrEmpty(createUserDto.Password))
            {
                return new BadRequestObjectResult("Invalid input data. Please ensure all fields are filled in.");
            }


            var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                return new ConflictObjectResult($"The email address is already registered. {existingUser}");
            }
            else
            {
                var passwordHash = HashPassword(createUserDto.Password);

                var user = CreateUserFactory.CreateUserEntity(createUserDto, passwordHash);

                await _userRepository.CreateUserAsync(user);

                return new CreatedResult(string.Empty, "User created successfully!");

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
            return new StatusCodeResult(500);
        }

    }

    private static string HashPassword(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }

    public async Task<IActionResult> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        try
        {
            if (updateUserDto != null)
            {
                var existingUser = await _userRepository.GetUserAsync(x => x.Id == updateUserDto.UserId);
                if (existingUser != null)
                {
                    var mappedEntity = UpdateUserFactory.UpdateUserEntity(updateUserDto, existingUser);
                    if (mappedEntity != null)
                    {
                        var result = await _userRepository.UpdateUserAsync(mappedEntity);
                        if (result != null)
                        {
                            var mappedDto = UpdateUserFactory.UpdateUserDto(existingUser);
                            if (mappedDto != null)
                            {
                                return new OkObjectResult(mappedDto);
                            }
                        }
                    }
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            else
            {
                return new BadRequestResult();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return new StatusCodeResult(500);
    }

    public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
    {
        try
        {
            var userEntities = await _userRepository.GetAllUsersAsync();

            if (userEntities == null || !userEntities.Any())
            {
                return Enumerable.Empty<GetUserDto>();
            }

            var userDtos = userEntities.Select(user => new GetUserDto
            {
                UserId = user.Id,
                FirstName = user.FirstName ?? "Inte angiven",
                LastName = user.LastName ?? "Inte angiven",
                Email = user.Email ?? "Inte angiven",
                PhoneNumber = user.PhoneNumber ?? "Inte angiven",
                ProfileImageUrl = user.ProfileImageUrl ?? "Inte angiven",
                Gender = user.Gender ?? "Inte angiven",
                Age = user.Age
			});
            return userDtos;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR :: {ex.Message}");
            return null!;
        }

    }

	public async Task<IActionResult> DeleteUserAsync(string userId)
	{
		try
		{
			if (userId != null)
			{
				var existingUser = await _userRepository.GetUserAsync(x => x.Id == userId);
				if (existingUser != null)
				{
					var result = await _userRepository.DeleteUserAsync(existingUser);
					if (result)
					{
						return new OkResult();
					}
				}
				else
				{
					return new NotFoundResult();
				}
			}
			else
			{
				return new BadRequestResult();
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("ERROR :: " + ex.Message);
		}
		return new StatusCodeResult(500);
	}

    public async Task<IActionResult> GetUserAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        try
        {

            if (predicate != null)
            {
                var userEntity = await _userRepository.GetUserAsync(predicate);

                if (userEntity != null) 
                {
                    var getUserDto = ReadUserFactory.GetUserDto(userEntity);

                    if (getUserDto !=null) 
                    {
                        return new OkObjectResult(getUserDto);
                    }
                    else
                    {
                        return new StatusCodeResult(500);
                    }
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            else
            {
                return new BadRequestResult();
            }

        }

        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return new StatusCodeResult(500);
    }
}


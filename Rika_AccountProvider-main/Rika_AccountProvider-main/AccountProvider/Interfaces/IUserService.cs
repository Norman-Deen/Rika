using AccountProvider.Entities;
using AccountProvider.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AccountProvider.Interfaces;

public interface IUserService
{
	Task<IActionResult> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<UserEntity?> GetUserByEmailAsync(string email);
    Task<IActionResult> CreateUserAsync(CreateUserDto createUserDto);
    Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
	Task<IActionResult> DeleteUserAsync(string userId);
    Task<IActionResult> GetUserAsync(Expression<Func<UserEntity, bool>> predicate);
}

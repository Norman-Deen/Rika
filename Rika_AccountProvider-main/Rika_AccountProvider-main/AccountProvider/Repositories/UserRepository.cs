using AccountProvider.Context;
using AccountProvider.Entities;
using AccountProvider.Interfaces;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccountProvider.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    private readonly DataContext _context = context;

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task CreateUserAsync(UserEntity userEntity)
    {
        try
        {
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }

    }

    public async Task<UserEntity> GetUserAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Users.FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                return result;

            }
            else
            {
                return null!;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Something went wrong", ex);
        }
    }

    public async Task<UserEntity?> UpdateUserAsync(UserEntity userEntity)
    {
        try
        {
            if (userEntity != null)
            {
                _context.Users.Update(userEntity);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return userEntity;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null;
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return Enumerable.Empty<UserEntity>();
    }

	public async Task<bool> DeleteUserAsync(UserEntity userEntity)
	{
		try
		{
			if (userEntity != null)
			{
				_context.Users.Remove(userEntity);
				var result = await _context.SaveChangesAsync();

				if (result > 0)
				{
					return true;
				}
                else
                {
                    return false;
                }
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("ERROR :: " + ex.Message);
		}

		return false;
	}
}


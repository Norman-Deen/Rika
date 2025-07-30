using AccountProvider.Entities;
using AccountProvider.Models;
using System.Diagnostics;

namespace AccountProvider.Factories;

public class ReadUserFactory
{
   public static GetUserDto GetUserDto(UserEntity userEntity)
    {
        try
        {
            if (userEntity != null)
            {
                return new GetUserDto
                {
                    UserId = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    Password = userEntity.Password!,
                    PhoneNumber = userEntity.PhoneNumber,
                    ProfileImageUrl = userEntity.ProfileImageUrl,
                    Age = userEntity.Age,
                    Gender = userEntity.Gender
                };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }
}

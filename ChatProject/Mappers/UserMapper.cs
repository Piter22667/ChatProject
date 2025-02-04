using ChatProject.Data;
using ChatProject.Dto.User;
using ChatProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this Models.User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                LastName = user.LastName
            };
        }


        public static User ToUsersFromCreateDto(this CreateUserDto createUsersDto)
        {
            return new User
            {
               Login = createUsersDto.Login,
               Password = createUsersDto.Password,
               Name = createUsersDto.Name,
               Surname = createUsersDto.Surname,
               LastName = createUsersDto.LastName

            };
        }



        public static IEnumerable<UserDto> getllUsers(ApplicationDbContext dbContext)
        {
            return dbContext.User
                .ToList()
                .Select(u => u.ToUserDto());
        }



        public static UserDto createUser(ApplicationDbContext dbContext, CreateUserDto createUserDto)
        {

            if(createUserDto == null)
            {
                throw new Exception("User data cannot be null.");
            }
            var user = createUserDto.ToUsersFromCreateDto();
            dbContext.User.Add(user);
            dbContext.SaveChanges();
            return user.ToUserDto();

        }

        public static UserDto getUserById(ApplicationDbContext dbContext, int id)
        {
            var user = dbContext.User.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                throw new Exception("User not found.");
            }
            return user.ToUserDto();
        }

    }
}

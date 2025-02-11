using ChatProject.Data;
using ChatProject.Dto.User;
using ChatProject.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отримати інформацію про всіх користувачів
        /// </summary>
        /// <returns>Список всіх користувачів</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(UserMapper.getllUsers(_context));
        }

        /// <summary>
        /// Отримати інформацію про користувача
        /// </summary>
        /// <param name="id">Id користувача</param>
        /// <returns>Інформація про користувача за Id</returns>
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var user = UserMapper.getUserById(_context, id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }  

        /// <summary>
        /// Додати нового користувача
        /// </summary>
        /// <param name="createUserDto">Інформація про нового користувача</param>
        /// <returns>Інформація про нового користувача</returns>
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var createdUser = UserMapper.createUser(_context, createUserDto);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

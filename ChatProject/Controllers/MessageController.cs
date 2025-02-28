using ChatProject.Data;
using ChatProject.Mappers;
using Microsoft.AspNetCore.Mvc;
using ChatProject.Dto.Message;

namespace ChatProject.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отримати всі повідомлення
        /// </summary>
        /// <returns>Список всіх повідомлень із усіх чатів</returns>
        [HttpGet]
        public IActionResult getAllMessage()
        {
            return Ok(MessageMapper.getAllMessage(_context));
        }

        /// <summary>
        ///Отримати повідомлення за його Id
        /// </summary>
        /// <param name="id">Id повідомлення</param>
        /// <returns>Текст повідомлення за Id</returns>
        [HttpGet("{id}")]
        public IActionResult getMessageById([FromRoute] int id)
        {
            try
            {
                var message = MessageMapper.getMessageById(_context, id);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Створити нове повідомлення
        /// </summary>
        /// <param name="createMessageDto">Текст нового повідомлення</param>
        /// <returns>Інформація про додане повідомлення</returns>
        [HttpPost]
        public async Task<IActionResult> createMessage([FromForm] CreateMessageDto createMessageDto)
        {
            try
            {
                var message = await MessageMapper.createMessage(_context, createMessageDto);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        // !!! перевірка розміру файлу, перевірка на розширення і зробити щоб користувач
        // міг завантажувати декілька файлів з однаковим іменем 
        //!!! (додати перевірку на те, щоб користувач міг потім завантажувати файл зі справжнім іменем,
        //а при збергіаннні в бд реалізувати якусь логіку для того, 
        //!!! щоб зберігати в бд з якимось рандомним іменем).
        }
    }
}

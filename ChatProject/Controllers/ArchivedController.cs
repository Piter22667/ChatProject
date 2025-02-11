using ChatProject.Data;
using Microsoft.AspNetCore.Mvc;
using ChatProject.Mappers;

namespace ChatProject.Controllers
{
    [Route("api/archivedMessage")]
    [ApiController]
    public class ArchivedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArchivedController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отримати всі повідолмення з архіних чатів
        /// </summary>
        /// <returns>Список повідомлень з усіх чатів в архіві</returns>
        [HttpGet]
        public IActionResult getAllArchivedMessages()
        {
            return Ok(ArchivedMapper.getAllArchived(_context));
        }

        /// <summary>
        /// Отримати повідомлення з архіву по id
        /// </summary>
        /// <param name="id">Айді чату</param>
        /// <returns>Повідомлення з id вказаним користувачем</returns>
        [HttpGet("{id}")]
        public IActionResult getArchivedMessageById([FromRoute] int id)
        {
            try
            {
                var archivedMessage = ArchivedMapper.getArchivedById(_context, id);
                return Ok(archivedMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}

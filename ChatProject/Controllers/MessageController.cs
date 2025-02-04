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

        [HttpGet]
        public IActionResult getAllMessage()
        {
            return Ok(MessageMapper.getAllMessage(_context));
        }

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

        [HttpPost]
        public IActionResult createMessage([FromBody] CreateMessageDto createMessageDto)
        {
            try
            {
                var message = MessageMapper.createMessage(_context, createMessageDto);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

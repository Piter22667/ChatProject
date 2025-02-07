using ChatProject.Data;
using Microsoft.AspNetCore.Mvc;
using ChatProject.Mappers;

namespace ChatProject.Controllers
{
    [Route("api/archivedMessage")]
    [ApiController]
    public class ArchivedMessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArchivedMessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getAllArchivedMessages()
        {
            return Ok(ArchivedMessageMapper.getAllArchivedMessages(_context));
        }

        [HttpGet("{id}")]
        public IActionResult getArchivedMessageById([FromRoute] int id)
        {
            try
            {
                var archivedMessage = ArchivedMessageMapper.getArchivedMessageById(_context, id);
                return Ok(archivedMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}

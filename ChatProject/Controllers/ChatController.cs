using ChatProject.Data;
using ChatProject.Dto.Chat;
using ChatProject.Dto.User;
using ChatProject.Mappers;
using ChatProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatProject.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllChats()
        {
            return Ok(ChatMapper.getAllChats(_context));
        }

        [HttpGet("{id}")]
        public IActionResult GetChatById([FromRoute] int id)
        {
            try
            {
                var chat = ChatMapper.getChatById(_context, id);
                return Ok(chat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateChatDto createChatDto)
        {
            try
            {
                var chat = ChatMapper.createChat(_context, createChatDto);
                return Ok(chat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateChat(int id, [FromBody] UpdateChatDto updateChatDto)
        {
            try
            {
                var updatedChat = ChatMapper.updateChat(_context, id, updateChatDto);
                return Ok(updatedChat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
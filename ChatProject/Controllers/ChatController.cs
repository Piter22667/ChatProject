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

        /// <summary>
        /// Отримати інформацію про всі чати
        /// </summary>
        /// <returns>Список всіх існуючих чатів та детальна інформація про них</returns>
        [HttpGet]
        public IActionResult GetAllChats()
        {
            return Ok(ChatMapper.getAllChats(_context));
        }

        /// <summary>
        /// Отримати чат за Id
        /// </summary>
        /// <param name="id">Id чату</param>
        /// <returns>Інформація про чат з Id</returns>
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

        /// <summary>
        /// Отримати всі повідомлення з чату
        /// </summary>
        /// <param name="id">Id чату</param>
        /// <returns>Всі повідомлення з чату з конекретним Id</returns>
        [HttpGet("{id}/messages")]
        public IActionResult GetAllMessagesFromChat([FromRoute] int id)
        {
            try
            {
                var messages = ChatMapper.getAllMessagesFromChat(_context, id);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Створити новий чат
        /// </summary>
        /// <param name="createChatDto">Інформація про чат</param>
        /// <returns>Інформація про доданий чат</returns>
        [HttpPost]
        public IActionResult CreateChat([FromBody] CreateChatDto createChatDto)
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



        /// <summary>
        /// Додати користувача до чату
        /// </summary>
        /// <param name="chatId">Id чату, до якого потрібно додати користувача</param>
        /// <param name="addUserToChatDto"></param>
        /// <returns>Повна інформація про чат з доданим користувачем</returns>
        [HttpPost("{chatId}/addUser")]
        public IActionResult AddUserToChat(int chatId, [FromBody] AddUserToChatDto addUserToChatDto)
        {
            try
            {
                var chat = ChatMapper.addUserToChat(_context, chatId, addUserToChatDto);
                return Ok(chat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }







            /// <summary>
            /// Оновити назву чату
            /// </summary>
            /// <param name="id">Id чату, який потрібно оновити</param>
            /// <returns>Інформація про оновлення чату</returns>
            [HttpPut("{id}/title")]
            public IActionResult UpdateChatTitle(int id, [FromBody] UpdateChatTitleDto updateChatDto)
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

            /// <summary>
            /// Відкрити або закрити чат для написання
            /// </summary>
            /// <param name="chatId">Id чату</param>
            /// <param name="updateChatActivityDto"></param>
            /// <param name="userId">Id користувача для перевірки прав доступу до зміни чату</param>
            /// <returns>Інформація про змінену активність чату</returns>
            [HttpPut("{chatId}/editActivity")]
            public IActionResult UpdateChatActivity(int chatId, [FromBody] UpdateChatActivityDto updateChatActivityDto)
            {
                try
                {
                    //var currentUserId = int.Parse(HttpContext.User.FindFirst("Id").Value);
                    var updatedChat = ChatMapper.updateChatActivity(_context, chatId, updateChatActivityDto);
                    return Ok(updatedChat);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            /// <summary>
            /// Перенести чат в архів
            /// </summary>
            /// <param name="chatId">Id чату </param>
            /// <param name="userId">Id користувача для перевірки прав доступу на перенесення чату в архів</param>
            /// <returns>Інформація про кількість перенесених повідомлень з чату з Id</returns>
            [HttpPut("{chatId}/archive")]
            public IActionResult TransferChatActivity(int chatId)
            {
                try
                {
                    //ApplicationDbContext dbContext, int id, int userId
                    var updatedChat = ChatMapper.archiveChat(_context, chatId);
                    return Ok(updatedChat);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            /// <summary>
            /// Видалити чат з архіву
            /// </summary>
            /// <param name="chatId">Id чату</param>
            /// <param name="userId">Id користувача для перевірки прав доступу на редагування інфорації про чат</param>
            /// <returns>Інформація про кількість перенесених повідомлень</returns>
            [HttpPut("{chatId}/unarchive")]
            public IActionResult UnarchiveChat(int chatId, [FromBody] UnarchiveChatDto unarchiveChatDto)
            {
                try
                {
                    var updatedChat = ChatMapper.unArchiveChat(_context, chatId, unarchiveChatDto);
                    return Ok(updatedChat);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }
    }
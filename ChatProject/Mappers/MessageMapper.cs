using ChatProject.Data;
using ChatProject.Dto.Chat;
using ChatProject.Dto.Message;
using ChatProject.Models;
namespace ChatProject.Mappers
{
    public static class MessageMapper
    {

        public static MessageDto ToMessageDto(this Models.Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                ChatId = message.ChatId,
                UserId = message.UserId,
                Content = message.Content,
                SentTime = message.SentTime,
            };
        }

        public static Message ToMessageFromCreateDto(this CreateMessageDto createMessageDto)
        {
            return new Message
            {
               Content = createMessageDto.Content,
                SentTime = createMessageDto.SentTime,
                ChatId = createMessageDto.ChatId,
                UserId = createMessageDto.UserId,
            };
        }
        /// <summary>
        /// Отримати всі повідомлення з усіх чатів
        /// </summary>
        /// <returns>Список всых повыдомлень з усіх чатів у форматі json</returns>
        public static IEnumerable<MessageDto> getAllMessage(ApplicationDbContext context)
        {
            return context.Messages.Select(c => c.ToMessageDto()).ToList()
               ;
        }

        /// <summary>
        /// Створити нове повідомлення
        /// </summary>
        /// <returns>Інформація про новий запис в таблиці повідомлень</returns>
        public static MessageDto createMessage(ApplicationDbContext context, CreateMessageDto createMessageDto)
        {
            if (createMessageDto == null)
            {
                throw new Exception("Chat data cannot be null.");
            }

            bool isUserInChat = context.Chats.Any(cu => cu.Id == createMessageDto.ChatId && cu.UserId == createMessageDto.UserId);

            if (!isUserInChat)
            {
                throw new UnauthorizedAccessException("user is not in chat");
            }

            var message = createMessageDto.ToMessageFromCreateDto();
            context.Messages.Add(message);
            context.SaveChanges();
            return message.ToMessageDto();
        }
        /// <summary>
        /// Отримання чату за id
        /// </summary>
        /// <returns>Інформація про чат за Id</returns>
        public static MessageDto getMessageById(ApplicationDbContext context, int id)
        {
            var message = context.Messages.Find(id);
            if (message == null)
            {
                throw new Exception("Chat not found.");
            }
            return message.ToMessageDto();
        }
    }
}

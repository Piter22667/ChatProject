using ChatProject.Data;
using ChatProject.Dto.Chat;
using ChatProject.Models;

namespace ChatProject.Mappers
{
    public static class ChatMapper
    {
        public static ChatDto ToChatDto(this Models.Chat chat)
        {

            return new ChatDto
            {
                Id = chat.Id,
                Title = chat.Title,
                IsClosed = chat.IsClosed,
                CreationTime = chat.CreationTime,
                UpdatedTime = chat.UpdatedTime
            };
        }

        public static Chat ToChatFromCreateDto(this CreateChatDto createChatDto)
        {
            return new Chat
            {
                Title = createChatDto.Title,
                IsClosed = createChatDto.IsClosed,
                CreationTime = createChatDto.CreationTime,
                UpdatedTime = createChatDto.UpdatedTime,
            };
        }

        public static UpdateChatDto ToChatUpdateDto(this Chat chat)
        {
            return new UpdateChatDto
            {
                Title = chat.Title,
                IsClosed = chat.IsClosed,

            };
        }



        public static IEnumerable<ChatDto> getAllChats(ApplicationDbContext dbContext)
        {
            return dbContext.Chats.Select(c => c.ToChatDto()).ToList();
        }

        public static ChatDto createChat(ApplicationDbContext dbContext, CreateChatDto createChatDto)
        {
            if (createChatDto == null)
            {
                throw new Exception("Chat data cannot be null.");
            }
            var chat = createChatDto.ToChatFromCreateDto();
            dbContext.Chats.Add(chat);
            dbContext.SaveChanges();
            return chat.ToChatDto();
        }
        /// <summary>
        /// Отримати чат по Id
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="id">Id</param>
        /// <returns>Користувач із вказаним айді</returns>
        /// <exception cref="Exception"></exception>
        public static ChatDto getChatById(ApplicationDbContext dbContext, int id)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == id);
            if (chat == null)
            {
                throw new Exception("Chat not found.");
            }
            return chat.ToChatDto();
        }
        
        public static UpdateChatDto updateChat(ApplicationDbContext dbContext, int id, UpdateChatDto updateChatDto)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == id);
            if (chat == null)
            {
                throw new Exception("Chat not found.");
            }
            chat.Title = updateChatDto.Title;
            chat.IsClosed = updateChatDto.IsClosed;
            chat.UpdatedTime = DateTime.UtcNow; // Примусово встановлюємо поточний UTC час
            dbContext.Chats.Update(chat);
            dbContext.SaveChanges();
            return chat.ToChatUpdateDto();
        }
    }
}

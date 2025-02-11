using Azure.Core;
using ChatProject.Data;
using ChatProject.Dto.Chat;
using ChatProject.Models;
using ChatProject.Dto.Message;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.Common;

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
                UserId = (int)chat.UserId,
                IsClosed = chat.IsClosed,
                CreationTime = chat.CreationTime,
                UpdatedTime = chat.UpdatedTime,
                Expiration = chat.Expiration,
                isArchived = chat.isArchived
            };
        }

        public static Chat ToChatFromCreateDto(this CreateChatDto createChatDto)
        {
            return new Chat
            {
                Title = createChatDto.Title,
                UserId = createChatDto.UserId,
                Expiration = createChatDto.Expiration,
                //IsClosed = createChatDto.IsClosed,
                //CreationTime = createChatDto.CreationTime,
                //UpdatedTime = createChatDto.UpdatedTime,
            }; // поле actualExpiration буде використовуватись тільки коли
               // користувач буде виконувати запит на закриття чату окремо
        }

        ///Розширюваний метод для отримання всхі повідомлень з конкретного чату
        public static IEnumerable<MessageDto> ToMessageDto(this IEnumerable<Models.Message> messages)
        {
            return messages.Select(m => m.ToMessageDto());
        }


        public static UpdateChatTitleDto ToChatUpdateDto(this Chat chat)
        {
            return new UpdateChatTitleDto
            {
                Title = chat.Title
            };
        }

        public static UpdateChatActivityDto ToChatUpdateActivityDto(this Chat chat)
        {
            return new UpdateChatActivityDto
            {
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

            if (!chat.Expiration.HasValue)
            {
                chat.Expiration = chat.CreationTime.AddDays(14);
            }

            //chat.Expiration ??= DateTime.UtcNow.AddDays(7);
            dbContext.Chats.Add(chat);
            dbContext.SaveChanges();

            return chat.ToChatDto();
        }
        /// <summary>
        /// Отримати чат по Id
        /// </summary>
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

        /// <summary>
        /// Отримати всі повідомлення з чату з Id
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="chatId">Id чату</param>
        /// <returns>Всі повідмолення з чату з конкретним Id</returns>
        public static IEnumerable<MessageDto> getAllMessagesFromChat(ApplicationDbContext dbContext, int chatId)
        {
           var messages = dbContext.Messages.Where(m => m.ChatId == chatId).ToList();
            if(messages == null)
            {
                throw new Exception("Messages not found in this chat.");
            }

            return messages.ToMessageDto();
        }



        public static UpdateChatTitleDto updateChat(ApplicationDbContext dbContext, int id, UpdateChatTitleDto updateChatDto)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == id);
            if (chat == null)
            {
                throw new Exception("Chat not found.");
            }
            chat.Title = updateChatDto.Title;
            //chat.IsClosed = updateChatDto.IsClosed;
            chat.UpdatedTime = DateTime.UtcNow; // Примусово встановлюємо поточний UTC час
            dbContext.Chats.Update(chat);
            dbContext.SaveChanges();
            return chat.ToChatUpdateDto();
        }

        public static UpdateChatActivityDto updateChatActivity(ApplicationDbContext dbContext, int id, UpdateChatActivityDto updateChatDto, int userId)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == id);
            if (chat == null)
            {
                throw new Exception("Chat not found.");
            }

            if (chat.UserId != userId)
            {
                throw new UnauthorizedAccessException("User is not allowed to close this chat.");
            }

            chat.IsClosed = updateChatDto.IsClosed;
            chat.UpdatedTime = DateTime.UtcNow; // Примусово встановлюємо поточний UTC час
            dbContext.Chats.Update(chat);
            dbContext.SaveChanges();
            return chat.ToChatUpdateActivityDto();
        }


        public static MessagesInChatToReturnDto archiveChat(ApplicationDbContext dbContext, int chatId, int userId)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                throw new Exception("Chat not found.");
            }
            if (chat.UserId != userId)
            {
                throw new UnauthorizedAccessException("User is not allowed to archive this chat.");
            }
            var messagesToArchive = dbContext.Messages.Where(m => m.ChatId == chatId).ToList();

            var archivedMessages = messagesToArchive.Select(message => new Archived
            {
                ChatId = message.ChatId,
                UserId = message.UserId,
                Content = message.Content,
                SentTime = message.SentTime
            }).ToList();

            dbContext.Chats.FirstOrDefault(c => c.Id == chatId).isArchived = true;
            // Додаємо всі повідомлення до таблиці Archived
            dbContext.Archived.AddRange(archivedMessages);

            // Видаляємо всі повідомлення з таблиці Messages
            dbContext.Messages.RemoveRange(messagesToArchive);

            // Зберігаємо зміни в базі даних
            dbContext.SaveChanges();
            return new MessagesInChatToReturnDto
            {
                ChatId = chat.Id,
                MessageCount = archivedMessages.Count,
            };
        }




        public static MessagesInChatToReturnDto unArchiveChat(ApplicationDbContext dbContext, int chatId, int userId, UnarchiveChatDto unarchiveChatDto)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                throw new Exception("Chat not found.");
            }
            if (chat.UserId != userId)
            {
                throw new UnauthorizedAccessException("User is not allowed to unarchive this chat.");
            }


            if (unarchiveChatDto.Expiration.HasValue)
            {
                chat.Expiration = unarchiveChatDto.Expiration.Value;
            }

            var messagesToUnarchive = dbContext.Archived.Where(m => m.ChatId == chatId).ToList();

            chat.isArchived = false;

            var unarchivedMessages = messagesToUnarchive.Select(message => new Message
            {
                ChatId = message.ChatId,
                UserId = message.UserId,
                Content = message.Content,
                SentTime = message.SentTime
            }).ToList();

            // Додаємо всі повідомлення до таблиці Messages
            dbContext.Messages.AddRange(unarchivedMessages);

            // Видаляємо всі повідомлення з таблиці Archived
            dbContext.Archived.RemoveRange(messagesToUnarchive);

            // Зберігаємо зміни в базі даних
            dbContext.SaveChanges();
            return new MessagesInChatToReturnDto
            {
                ChatId = chat.Id,
                MessageCount = unarchivedMessages.Count,
            };
        }



        //public static CloseChatDto closeChat(ApplicationDbContext dbContext, int chatId)
        //{
        //    var chat = dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
        //    if (chat == null)
        //    {
        //        throw new Exception("Chat not found.");
        //    }
        //    return chat.ToChatDto();
        //}


    }
}


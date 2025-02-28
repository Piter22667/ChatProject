using Azure.Core;
using ChatProject.Data;
using ChatProject.Dto.Chat;
using ChatProject.Models;
using ChatProject.Dto.Message;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.Common;
using ChatProject.Controllers;
using System;
using ChatProject.Dto.Files;

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



        public static UpdatedChatReturnAllInfoDto ToUpdatedChatReturnAllInfoDto(this  Chat chat, List<int> userIds)
        {
            return new UpdatedChatReturnAllInfoDto
            {
                Title = chat.Title,
                UserId = (int)chat.UserId,
                IsClosed = chat.IsClosed,
                CreationTime = chat.CreationTime,
                UpdatedTime = chat.UpdatedTime,
                Expiration = chat.Expiration,
                isArchived = chat.isArchived,
                UserIds = userIds ?? new List<int>() // Заглушка, якщо null


            };
        }

        public static UsersListAddedToChatDto usersListAddedToChatDto(this Chat chat, List<int> userIds)
        {
            return new UsersListAddedToChatDto
            {
                UserIds = userIds
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


        //public static UpdateChatTitleDto ToChatUpdateDto(this Chat chat)
        //{
        //    return new UpdateChatTitleDto
        //    {
        //        Title = chat.Title
        //    };
        //}

        /// <summary>
        /// Інформація про чат після оновлення
        /// </summary>
        /// <returns>Інформація про чат після оновлння</returns>
        public static ResultUpdateChatTitleDto resultUpdateChatTitle(this Chat chat)
        {

           return new ResultUpdateChatTitleDto
            {
                UserId = (int)chat.UserId,
                CreationTime = chat.CreationTime,
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


        /// <summary>
        /// Отримати інформацію про всі чати
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns>Json масив з інформацією про всі чати</returns>
        public static IEnumerable<UpdatedChatReturnAllInfoDto> getAllChats(ApplicationDbContext dbContext)
        {
            var chats = dbContext.Chats.ToList();

            var chatDtos = chats.Select(chat =>
            {
                var userIds = dbContext.ChatUsers
                    .Where(c => c.ChatId == chat.Id && c.UserId.HasValue)
                    .Select(c => c.UserId.Value)
                    .ToList();

                return chat.ToUpdatedChatReturnAllInfoDto(userIds);
            }).ToList();
            return chatDtos;
        }
        /// <summary>
        /// Створення запису про новий чат
        /// </summary>
        /// <returns>Інформацію про створений чат</returns>
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
        /// Додати нового користувача до чату
        /// </summary>
        /// <returns>Повна інформація про чат з доданим користувачем</returns>
        /// <exception cref="Exception"></exception>
        public static UsersListAddedToChatDto addUserToChat(ApplicationDbContext dbContext, int chatId, AddUserToChatDto addUserToChatDto)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
            if(chat == null)
            {
                throw new Exception("Chat not found.");
            }


            var chatUser = new ChatUsers
            {
                ChatId = chatId,
                UserId = addUserToChatDto.UserId
            };

            bool isUserAlredyExistInChat = dbContext.ChatUsers.Any(c => c.ChatId == chatId && c.UserId == addUserToChatDto.UserId);
            if (isUserAlredyExistInChat)
            {
                throw new Exception("User already exist in this chat");
            } // Перевірка на існування користувача в чаті

            dbContext.ChatUsers.Add(chatUser);
            dbContext.SaveChanges();

            var userIds = dbContext.ChatUsers.Where(c => c.ChatId == chat.Id && c.UserId.HasValue)
                .Select(c => c.UserId.Value)
                .ToList(); //Збираємо всіх користувачів які належать до конкретного чату
            return chat.usersListAddedToChatDto(userIds);
        }

        /// <summary>
        /// Отримати чат по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Користувач із вказаним айді</returns>
        /// <exception cref="Exception"></exception>
        public static UpdatedChatReturnAllInfoDto getChatById(ApplicationDbContext dbContext, int id)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == id);
            if (chat == null)
            {
                throw new Exception("Chat not found.");
            }

            var attachedFiles = dbContext.ChatFileConnections.Where(cfc => cfc.ChatId == chat.Id).Select(cfc => new AttachedFileDto
            {
                StreamId = cfc.ChatFile.StreamId,
                FileName = cfc.ChatFile.Name,

            }).ToList();

            var userIds = dbContext.ChatUsers
                .Where(с => с.ChatId == chat.Id && с.UserId.HasValue) 
                .Select(с => с.UserId.Value) // Перетворюємо int? -> int
                .ToList();

            return new UpdatedChatReturnAllInfoDto
            {
                Title = chat.Title,
                UserId = (int)chat.UserId,
                IsClosed = chat.IsClosed,
                CreationTime = chat.CreationTime,
                UpdatedTime = chat.UpdatedTime,
                Expiration = chat.Expiration,
                isArchived = chat.isArchived,
                UserIds = userIds,
                AttachedFiles = attachedFiles

            };
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


        /// <summary>
        /// Оновлення заголовку чату
        /// </summary>
        /// <returns>Інформація про чат пілся оновлення</returns>
        public static ResultUpdateChatTitleDto updateChat(ApplicationDbContext dbContext, int id, UpdateChatTitleDto updateChatDto)
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
            return chat.resultUpdateChatTitle();
        }

        /// <summary>
        /// Редагування активності чату(відкритий\закритий) для написання повідомлень
        /// </summary>
        /// <returns>Інформація про чат після оновлення</returns>
        public static UpdateChatActivityDto updateChatActivity(ApplicationDbContext dbContext, int id, UpdateChatActivityDto updateChatDto)
        {
            int userId = 1; //Заглушка під юзера
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

        /// <summary>
        /// Ручна архівація повідомлень з чату за Id
        /// </summary>
        /// <param name="chatId">Id чату, який потрібно архівувати </param>
        /// <returns>Кількість архівованих повідмомлень з чату</returns>
        public static MessagesInChatToReturnDto archiveChat(ApplicationDbContext dbContext, int chatId)
        {
            int userId = 1; //Заглушка під юзер айді
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



        /// <summary>
        /// Роззархівування чату за Id
        /// </summary>
        /// <returns>Кількість розархівованих повідомлень з чату за Id</returns>
        public static MessagesInChatToReturnDto unArchiveChat(ApplicationDbContext dbContext, int chatId, UnarchiveChatDto unarchiveChatDto)
        {
            int userId = 1; //Заглушка під юзер айді
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


using ChatProject.Data;
using ChatProject.Dto.Chat;
using ChatProject.Dto.Files;
using ChatProject.Dto.Message;
using ChatProject.Models;
using ChatProject.Models.Files;
using Microsoft.EntityFrameworkCore;
namespace ChatProject.Mappers
{
    public static class MessageMapper
    {

        public static MessageWithAttachedFilesDto ToMessageDto(this Models.Message message)
        {
            return new MessageWithAttachedFilesDto
            {
                Id = message.Id,
                ChatId = message.ChatId,
                UserId = message.UserId,
                Content = message.Content,
                SentTime = message.SentTime,
            };
        }

        public static MessageWithAttachedFilesDto ToMessageListFilesDto(this Models.Message message)
        {
            return new MessageWithAttachedFilesDto
            {
                Id = message.Id,
                ChatId = message.ChatId,
                UserId = message.UserId,
                Content = message.Content,
                SentTime = message.SentTime,
                AttachedFiles = message.ChatFileConnections.Where(cfc => cfc.MessageId == message.Id).Select(cfc => new AttachedFileDto
                {
                    StreamId = cfc.ChatFile.StreamId,
                    FileName = cfc.ChatFile.Name,
                }).ToList()

            };
        }


        public static MessageDto ToMessageBasicDto(this Models.Message message)
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




        //public static MessageWithAttachedFilesDto toMessageWithAttachedFilesDto(this Models.Message message)
        //{
        //    return new MessageWithAttachedFilesDto
        //    {
        //        Id = message.Id,
        //        ChatId = message.ChatId,
        //        UserId = message.UserId,
        //        Content = message.Content,
        //        SentTime = message.SentTime,
        //        StreamId = 
        //    }
        //}




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



        public static Message ToMessageWithFilesFromCreateDto(this CreateMessageWithFileDto createMessageDto)
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
        public static IEnumerable<MessageWithAttachedFilesDto> getAllMessage(ApplicationDbContext context)
        {


            //var attachedFiles = context.ChatFileConnections.Select(cfc => new AttachedFileDto
            //{
            //    StreamId = cfc.ChatFile.StreamId,
            //    FileName = cfc.ChatFile.Name,
            //}).ToList();


            return context.Messages
                 .Include(m => m.ChatFileConnections)
                .ThenInclude(cfc => cfc.ChatFile)  //додаткові включення звязків для успішного виконання пошуку в дто (без них не спрацює пошук в дто)
                .Select(c => c.ToMessageListFilesDto()).ToList();
        }


        public static IEnumerable<MessageWithAttachedFilesDto> getAllMessagesFromChat(ApplicationDbContext context, int id)
        {
            var messages = context.Messages.Where(c => c.ChatId == id)
                .Include(cf => cf.ChatFileConnections)
                .ThenInclude(cfc => cfc.ChatFile)
                .Select(c => c.ToMessageListFilesDto());
            if (context.Chats.Find(id) == null)
            {
                throw new Exception("Chat not found.");
            }
            if (messages == null)
            {
                throw new Exception("Chat is empty");
            }

            return messages;
        }



        /// <summary>
        /// Створити нове повідомлення
        /// </summary>
        /// <returns>Інформація про новий запис в таблиці повідомлень</returns>
        public static async Task<MessageWithAttachedFilesDto> createMessage(ApplicationDbContext context, CreateMessageWithFileDto createMessageWithFileDto)
        {
            if (createMessageWithFileDto == null)
            {
                throw new Exception("Chat data cannot be null.");
            }

            bool isUserInChat = context.Chats.Any(cu => cu.Id == createMessageWithFileDto.ChatId && cu.UserId == createMessageWithFileDto.UserId);
            if (!isUserInChat)
            {
                throw new UnauthorizedAccessException("user is not in chat");
            } // перевірка на існування користувача в чаті (якщо ні => помилка про відправку повідомлення)

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var message = createMessageWithFileDto.ToMessageWithFilesFromCreateDto();
                    context.Messages.Add(message);
                    await context.SaveChangesAsync();

                    if (createMessageWithFileDto.File != null)
                    {
                        await ChatFileMapper.SaveChatFileAsync(createMessageWithFileDto.File, message.Id, context);
                    } //Спочатку додаємо текстове повідомлення , а потім файл, якщо він прикріплений, задля того, щоб мати доступ до айді повідомлення
                    //яке заповнюється автоматично при додаванні в бд

                    await transaction.CommitAsync();
                    return message.ToMessageDto();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            //await context.SaveChangesAsync();
            //await ChatFileMapper.SaveChatFileAsync(createMessageWithFileDto, context);
            //return message.ToMessageDto();
        }
        /// <summary>
        /// Отримання чату за id
        /// </summary>
        /// <returns>Інформація про чат за Id</returns>
        public static MessageWithAttachedFilesDto getMessageById(ApplicationDbContext context, int id)
        {
            var message = context.Messages.Find(id);
            if (message == null)
            {
                throw new Exception("Message not found.");
            }

            var attachedFiles = context.ChatFileConnections.Where(cfc => cfc.MessageId == message.Id).Select(cfc => new AttachedFileDto
            {
                StreamId = cfc.ChatFile.StreamId,
                FileName = cfc.ChatFile.Name,

            }).ToList();

            return new MessageWithAttachedFilesDto
            {
                Id = message.Id,
                ChatId = message.ChatId,
                UserId = message.UserId,
                Content = message.Content,
                SentTime = message.SentTime,
                AttachedFiles = attachedFiles
            };
        }
    }
}

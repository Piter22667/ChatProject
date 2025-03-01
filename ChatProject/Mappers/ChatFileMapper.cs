using ChatProject.Data;
using ChatProject.Dto.Message;
using ChatProject.Models.Files;
using System.Runtime.Intrinsics.Arm;
using System.IO;
using System.Security.Cryptography;
using ChatProject.Models;

namespace ChatProject.Mappers
{
    public static class ChatFileMapper
    {
        public static async Task SaveChatFileAsync(IFormFile file, int messageId, ApplicationDbContext context)
        {
            //var file = createMessageWithFileDto.File;
            var streamId = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var fileNameWithTimestamp = fileName + "_" + timestamp + extension; //Унікалізація назви завантаженого файлу в бд

            var fileObject = new ChatFile
            {
                StreamId = streamId,
                Name = fileNameWithTimestamp,
                FileStream = await ConvertFileToBytes(file),
                CreationTime = DateTimeOffset.UtcNow,
            }; // перетворення в модель для збереження в бд


            await context.ChatFile.AddAsync(fileObject);
            await context.SaveChangesAsync();


            var chatFileConnection = new ChatFileConnections
            {
                MessageId = messageId,
                FileId = fileObject.StreamId,
            }; // створення звя'язків між файлом і чатом в таблиці ChatFileConnections
            await context.ChatFileConnections.AddAsync(chatFileConnection);
            await context.SaveChangesAsync();
        }
        //ддавання timestamp для унікальності імені файлу
        //context.ChatFile.Add(fileObject);
        //context.ChatFileConnections.Add(chatFileConnection);
        //await context.SaveChangesAsync();

        public static async Task<byte[]> ConvertFileToBytes(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
    }
}

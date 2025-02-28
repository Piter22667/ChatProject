using ChatProject.Data;
using ChatProject.Dto.Message;
using ChatProject.Models.Files;
using System.Runtime.Intrinsics.Arm;
using System.IO;
using System.Security.Cryptography;

namespace ChatProject.Mappers
{
    public static class ChatFileMapper
    {
        public static async Task SaveChatFileAsync(CreateMessageDto createMessageDto, ApplicationDbContext context)
        {
            var file = createMessageDto.File;
            var streamId = Guid.NewGuid();

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var fileNameWithTimestamp = fileName + "_" + timestamp + extension;
            //ддавання timestamp для унікальності імені файлу

            var fileObject = new ChatFile
            {   
                StreamId = streamId,
                Name = fileNameWithTimestamp,
                FileStream = await ConvertFileToBytes(file),
                CreationTime = DateTimeOffset.UtcNow,
            }; // перетворення в модель для збереження в бд
            var chatFileConnection = new ChatFileConnections
            {
                ChatId = createMessageDto.ChatId,
                FileId = streamId,
            }; // створення звя'язків між файлом і чатом в таблиці ChatFileConnections
            context.ChatFile.Add(fileObject);
            context.ChatFileConnections.Add(chatFileConnection);
            await context.SaveChangesAsync();
        }

        private static async Task<byte[]> ConvertFileToBytes(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

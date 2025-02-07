using ChatProject.Data;
using ChatProject.Dto.ArchivedMessage;
using ChatProject.Models;

namespace ChatProject.Mappers
{
    public static class ArchivedMessageMapper
    {
        public static ArchivedMessageDto ToArchivedMessageDto(this Models.ArchivedMessage archivedMessage)
        {
            return new ArchivedMessageDto
            {
                Id = archivedMessage.Id,
                ChatId = archivedMessage.ChatId,
                UserId = archivedMessage.UserId,
                Content = archivedMessage.Content,
                SentTime = archivedMessage.SentTime,
                ExpiredAt = archivedMessage.ExpiredAt

            };
        }

        public static ArchivedMessage ToArchivedMessageFromDto(this CreateArchivedMessageDto createArchivedMessageDto)
        {
            return new ArchivedMessage
            {
                ChatId = createArchivedMessageDto.ChatId,
                UserId = createArchivedMessageDto.UserId,
                Content = createArchivedMessageDto.Content,
                SentTime = createArchivedMessageDto.SentTime,
                ExpiredAt = createArchivedMessageDto.ExpiredAt
            };
        }

        public static IEnumerable<ArchivedMessageDto> getAllArchivedMessages(ApplicationDbContext dbContext)
        {
            return dbContext.ArchivedMessages.Select(m => m.ToArchivedMessageDto()).ToList();
        }

        public static ArchivedMessageDto getArchivedMessageById(ApplicationDbContext dbContext, int id)
        {
            var archivedMessage = dbContext.ArchivedMessages.FirstOrDefault(m => m.Id == id);
            if (archivedMessage == null)
            {

                throw new Exception("Archived message not found.");
            }
            return archivedMessage.ToArchivedMessageDto();

        }
    }
}

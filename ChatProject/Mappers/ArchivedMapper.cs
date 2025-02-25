using ChatProject.Data;
using ChatProject.Dto.ArchivedMessage;
using ChatProject.Models;

namespace ChatProject.Mappers
{
    public static class ArchivedMapper
    {
        public static ArchivedDto ToArchivedDto(this Models.Archived archivedMessage)
        {
            return new ArchivedDto
            {
                Id = archivedMessage.Id,
                ChatId = archivedMessage.ChatId,
                UserId = archivedMessage.UserId,
                Content = archivedMessage.Content,
                SentTime = archivedMessage.SentTime,
            };
        }

        public static Archived ToArchivedFromDto(this CreateArchivedDto createArchivedMessageDto)
        {
            return new Archived
            {
                ChatId = createArchivedMessageDto.ChatId,
                UserId = createArchivedMessageDto.UserId,
                Content = createArchivedMessageDto.Content,
                SentTime = createArchivedMessageDto.SentTime,
            };
        }
        /// <summary>
        /// Отримати масив усіх повідомлень з архівованих чатів
        /// </summary>
        /// <returns>масив усіх повідомлень з архівованих чатів</returns>
        public static IEnumerable<ArchivedDto> getAllArchived(ApplicationDbContext dbContext)
        {
            return dbContext.Archived.Select(m => m.ToArchivedDto()).ToList();
        }

        /// <summary>
        /// Отримати архівоване повідомлення за Id
        /// </summary>
        /// <returns>Інформацію про повідомлення з архівованого чату за Id</returns>
        public static ArchivedDto getArchivedById(ApplicationDbContext dbContext, int id)
        {
            var archivedMessage = dbContext.Archived.FirstOrDefault(m => m.Id == id);
            if (archivedMessage == null)
            {

                throw new Exception("Archived message not found.");
            }
            return archivedMessage.ToArchivedDto();

        }
    }
}

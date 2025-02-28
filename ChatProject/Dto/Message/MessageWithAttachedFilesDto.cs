using ChatProject.Dto.Files;

namespace ChatProject.Dto.Message
{
    public class MessageWithAttachedFilesDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }

        public List<AttachedFileDto> AttachedFiles { get; set; } = new List<AttachedFileDto>(); // Масив файлів

    }
}

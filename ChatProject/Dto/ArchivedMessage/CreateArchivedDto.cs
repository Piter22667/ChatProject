namespace ChatProject.Dto.ArchivedMessage
{
    /// <summary>
    /// Інформація про перенесений чат
    /// </summary>
    public class CreateArchivedDto
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
    }
}

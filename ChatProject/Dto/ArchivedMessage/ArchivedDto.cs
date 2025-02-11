namespace ChatProject.Dto.ArchivedMessage
{
    /// <summary>
    /// Інформація про чат в архіві
    /// </summary>
    public class ArchivedDto
    {
        public int Id { get; set; }
        public  int ChatId{ get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
    }
}

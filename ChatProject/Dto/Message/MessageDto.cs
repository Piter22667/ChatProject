namespace ChatProject.Dto.Message
{
    /// <summary>
    /// Інформація про додане повідомлення
    /// </summary>
    public class MessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
    }
}

namespace ChatProject.Dto.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}

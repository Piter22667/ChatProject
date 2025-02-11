namespace ChatProject.Dto.Message
{
    /// <summary>
    /// Інформація про створене повідомлення
    /// </summary>
    public class CreateMessageDto
    {
       
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }

    }
}

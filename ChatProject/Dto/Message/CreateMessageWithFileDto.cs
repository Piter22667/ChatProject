namespace ChatProject.Dto.Message
{
    public class CreateMessageWithFileDto
    {
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public IFormFile? File { get; set; }
    }
}

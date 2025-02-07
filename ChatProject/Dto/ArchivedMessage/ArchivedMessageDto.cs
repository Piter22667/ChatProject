namespace ChatProject.Dto.ArchivedMessage
{
    public class ArchivedMessageDto
    {
        public int Id { get; set; }
        public  int ChatId{ get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}

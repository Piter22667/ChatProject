namespace ChatProject.Dto.Chat
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public Boolean IsClosed { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdatedTime{ get; set; }
    }
}

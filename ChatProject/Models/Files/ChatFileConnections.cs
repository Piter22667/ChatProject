namespace ChatProject.Models.Files
{
    public class ChatFileConnections
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Guid  FileId { get; set; }


        public  Chat Chat { get; set; }
        public  ChatFile ChatFile { get; set; }
    }
}

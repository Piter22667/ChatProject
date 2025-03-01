namespace ChatProject.Models.Files
{
    public class ChatFileConnections
    {
        public int Id { get; set; }
        public int? MessageId { get; set; } //foreign key to connect specific file to message

        public Guid  FileId { get; set; }

        //public  Chat Chat { get; set; }
        public Message Message { get; set; }
        public  ChatFile ChatFile { get; set; }
    }
}

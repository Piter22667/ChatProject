namespace ChatProject.Models.Files
{
    public class ChatFileNameMap
    {
        public Guid Id { get; set; }
        public string HashedName { get; set; }
        public string OriginalName { get; set; }
        public int MessageId { get; set; }
        public Guid? FileId { get; set; }


        // Зв'язок 1:1 із ChatFile
        public virtual ChatFile ChatFile { get; set; }
        public virtual Message Message { get; set; } //звязок 1:N з Message

    }
}

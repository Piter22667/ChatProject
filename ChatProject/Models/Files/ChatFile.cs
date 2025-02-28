namespace ChatProject.Models.Files
{
    public class ChatFile
    {
        public Guid StreamId { get; set; }
        public string Name { get; set; }
        public byte[] FileStream { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset LastAccessTime { get; set; }
        public string  FileType{ get; set; }

        public virtual ICollection<ChatFileConnections> ChatFileConnections { get; set; }


        // Зв’язок 1:1 із ChatFileNameMap
        public virtual ICollection<ChatFileNameMap> ChatFileNameMap { get; set; }
    }
}

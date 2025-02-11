using System.ComponentModel;

namespace ChatProject.Dto.Chat
{
    /// <summary>
    /// Інофрмація про доданий чат
    /// </summary>
    public class ChatDto
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public Boolean IsClosed { get; set; }
        public int UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdatedTime{ get; set; }
        public DateTime? Expiration { get; set; }
        [DefaultValue(false)]
        public Boolean isArchived { get; set; }
    }
}

using System.ComponentModel;

namespace ChatProject.Dto.Chat
{

    /// <summary>
    /// Інформація про новий чат
    /// </summary>
    public class CreateChatDto
    {
        public string Title{ get; set; }
        //public Boolean IsClosed { get; set; }
        public int? UserId { get; set; }
        //public DateTime CreationTime { get; set; }
        //public DateTime UpdatedTime{ get; set; }
        public DateTime? Expiration { get; set; }
        //public DateTime? ActualExpiration { get; set; }
    }
}

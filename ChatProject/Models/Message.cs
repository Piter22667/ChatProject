using ChatProject.Models.Files;

namespace ChatProject.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }

        // Navigation properties
        public User User { get; set; } // одне повідомлення належить тільки конкретному юзеру
        public Chat Chat { get; set; } // одне повідомлення належить тільки конкретному чату

        //public ICollection<ChatFileNameMap> ChatFileNameMap { get; set; } = new List<ChatFileNameMap>(); // Колекція прив’язаних файлів

        public virtual ICollection<ChatFileConnections> ChatFileConnections { get; set; } = new List<ChatFileConnections>();


    }
}

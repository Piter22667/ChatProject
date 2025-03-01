using ChatProject.Models.Files;

namespace ChatProject.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Boolean IsClosed { get; set; }
        public int? UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? Expiration { get; set; }
        public Boolean isArchived { get; set; }

        //Navition properties   
        public User User { get; set; } // один чат належить тільки конкретному юзеру
        public ICollection<Message> Messages { get; set; } // один чат може мати багато повідомлень
        public ICollection<Archived> ArchivedMessages { get; set; } // один чат може мати багато архівованих повідомлень
        public ICollection<ChatUsers> ChatUsers { get; set; } = new List<ChatUsers>();
    
    }
}

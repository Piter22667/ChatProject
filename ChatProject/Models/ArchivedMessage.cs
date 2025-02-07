namespace ChatProject.Models
{
    public class ArchivedMessag
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public DateTime ExpiredAt { get; set; }

        //Navigation properties
        public User User { get; set; } // одне архівоване повідомлення належить тільки конкретному юзеру
        public Chat Chat { get; set; } // одне архівоване повідомлення належить тільки конкретному чату

    }
}

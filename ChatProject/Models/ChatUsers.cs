namespace ChatProject.Models
{
    public class ChatUsers
    {
        public  int Id { get; set; }
        public int? ChatId { get; set; }
        public  int? UserId { get; set; }

        //Navigation properties
        public User User { get; set; } //User (1) → (∞) (кожен користувач може бути у багатьох чатах)
        public Chat Chat { get; set; } //Chat(1) → (∞) ChatUsers(кожен чат може мати багато користувачів)
    }
}

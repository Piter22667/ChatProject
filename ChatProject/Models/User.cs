namespace ChatProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? LastName { get; set; }

        // Navigation properties
        public ICollection<Message> Messages { get; set; } = new List<Message>(); // один юзер може мати багато повідомлень
        public ICollection<ArchivedMessage> ArchivedMessages { get; set; } = new List<ArchivedMessage>(); // один юзер може мати багато архівованих повідомлень
    }
}

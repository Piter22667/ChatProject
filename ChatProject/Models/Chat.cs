namespace ChatProject.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Boolean IsClosed { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdatedTime { get; set; }


        //Navition properties
        public ICollection<Message> Messages { get; set; } // один чат може мати багато повідомлень
        public ICollection<ArchivedMessage> ArchivedMessages { get; set; } // один чат може мати багато архівованих повідомлень
    }
}

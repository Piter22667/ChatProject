namespace ChatProject.Dto.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User.UserDto User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

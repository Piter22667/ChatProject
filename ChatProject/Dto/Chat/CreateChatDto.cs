using System.ComponentModel;

namespace ChatProject.Dto.Chat
{
    public class CreateChatDto
    {
        public string Title{ get; set; }
        [DefaultValue(false)]
        public Boolean IsClosed { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdatedTime{ get; set; }
    }
}

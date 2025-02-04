using System.ComponentModel;

namespace ChatProject.Dto.Chat
{
    public class UpdateChatDto
    {
        public string Title{ get; set; }
        [DefaultValue(false)]
        public Boolean IsClosed { get; set; }
    }
}

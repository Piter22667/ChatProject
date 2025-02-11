using System.ComponentModel;

namespace ChatProject.Dto.Chat
{
    public class UpdateChatActivityDto
    {
        [DefaultValue(false)]
        public Boolean IsClosed { get; set; }
    }
}

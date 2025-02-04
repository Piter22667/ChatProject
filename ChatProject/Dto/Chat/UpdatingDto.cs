using System.ComponentModel;

namespace ChatProject.Dto.Chat
{
    public class UpdatingDto
    {
        public string Title { get; set; }
        [DefaultValue(false)]
        public Boolean IsClosed { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}

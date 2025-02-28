using ChatProject.Dto.Files;
using System.ComponentModel;

namespace ChatProject.Dto.Chat
{
    public class UpdatedChatReturnAllInfoDto
    {
        public string Title { get; set; }
        public Boolean IsClosed { get; set; }
        public int UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? Expiration { get; set; }
        [DefaultValue(false)]
        public Boolean isArchived { get; set; }
        public List<int>? UserIds { get; set; }



        //отримання інформацій про файли
        public List<AttachedFileDto> AttachedFiles { get; set; } = new List<AttachedFileDto>(); // Масив файлів


    }
}

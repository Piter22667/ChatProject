namespace ChatProject.Dto.User
{   
    /// <summary>
    /// Інформація про користувача
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
    }
}

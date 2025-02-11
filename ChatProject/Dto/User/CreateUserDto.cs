namespace ChatProject.Dto.User
{
    /// <summary>
    /// інформація про доданого користувача
    /// </summary>
    public class CreateUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? LastName { get; set; }

    }
}

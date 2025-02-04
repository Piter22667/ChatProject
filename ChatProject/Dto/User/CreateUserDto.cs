namespace ChatProject.Dto.User
{
    public class CreateUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? LastName { get; set; }

    }
}

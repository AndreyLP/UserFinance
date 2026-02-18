namespace UserService.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        private User() { }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}

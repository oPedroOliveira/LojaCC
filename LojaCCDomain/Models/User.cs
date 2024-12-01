namespace LojaCCDomain.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string ClienteId { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}

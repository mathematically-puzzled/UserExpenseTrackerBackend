namespace Domain
{
    public class UserSalt
    {
        public Guid Id { get; set; }
        public string PasswordSalt { get; set; }

        public User User { get; set; }
    }
}

namespace Domain
{
    public class UserSalt
    {
        public int Id { get; set; }
        public string PasswordSalt { get; set; }

        public User User { get; set; }
    }
}

namespace Application.Models.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Currency { get; set; }
        public long BankBalance { get; set; }
        public string EmailId { get; set; }
        public long MobileNumber { get; set; }
        public int PasswordLength { get; set; }
        public string BearerToken { get; set; }
    }
}

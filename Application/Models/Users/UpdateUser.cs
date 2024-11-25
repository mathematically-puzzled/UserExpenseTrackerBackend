namespace Application.Models.Users
{
    public class UpdateUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public long BankBalance { get; set; }
        public long MobileNumber { get; set; }
    }
}

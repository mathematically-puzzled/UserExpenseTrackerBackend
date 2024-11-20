namespace Domain
{
    public class Expenses
    {
        public Guid Id { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string ExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }

        public User User { get; set; }
    }
}

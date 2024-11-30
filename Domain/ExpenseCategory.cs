namespace Domain
{
    public class ExpenseCategory
    {
        public Guid Id { get; set; }
        public bool FromAdmin { get; set; }
        public string ExpenseType { get; set; }
        public User User { get; set; }
    }
}

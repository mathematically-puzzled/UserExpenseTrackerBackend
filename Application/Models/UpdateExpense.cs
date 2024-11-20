namespace Application.Models
{
    public class UpdateExpense
    {
        public Guid Id { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string ExpenseType { get; set; }
        public Guid UserId { get; set; }
    }
}

namespace Application.Models.Expense
{
    public class NewExpense
    {
        public long Amount { get; set; }
        public string Description { get; set; }
        public string ExpenseType { get; set; }
        public Guid UserId { get; set; }
    }
}

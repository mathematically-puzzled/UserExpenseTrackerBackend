namespace Application.Models.Expense
{
    public class ExpenseRequestModel
    {
        public Guid UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> ExpenseType { get; set; }
    }
}

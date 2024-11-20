namespace Application.Models
{
    public class FetchExpense
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
        public string ExpenseType { get; set; }
    }
}

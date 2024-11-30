namespace Application.Models.Expense
{
    public class ExpenseRequestModel
    {
        public Guid UserId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<string> ExpenseType { get; set; }
    }
}

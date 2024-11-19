namespace Domain
{
    public class ExpenseCategories
    {
        public int Id { get; set; }
        public bool FromAdmin { get; set; }
        public string ExpenseType { get; set; }
        public User User { get; set; }
    }
}

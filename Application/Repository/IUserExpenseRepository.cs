using Application.Models.Expense;
using Domain;

namespace Application.Repository
{
    public interface IUserExpenseRepository
    {
        public Task<List<Expenses>> FetchUserExpensesAsync(ExpenseRequestModel RequestModel);
        public Task AddNewExpenseAsync(Expenses UserExpense);
        public Task UpdateExpenseAsync(Expenses UserExpense);
        public Task DeleteExpenseAsync(Guid UserExpenseId);
        public Task<Expenses> GetExpenseByGuidAsync(Guid UserExpenseId);
    }
}

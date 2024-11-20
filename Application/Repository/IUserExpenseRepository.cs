using Application.Models;
using Domain;

namespace Application.Repository
{
    public interface IUserExpenseRepository
    {
        Task AddNewExpenseTypeAsync(ExpenseCategories expenseType);
        Task AddNewExpenseAsync(Expenses expense);
        Task UpdateExpenseAsync(Expenses expense);
        Task DeleteExpenseAsync(Expenses expense);
        Task FetchExpenseAsync(FetchExpense expense);
    }
}

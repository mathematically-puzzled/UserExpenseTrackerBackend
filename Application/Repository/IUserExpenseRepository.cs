using Application.Models;

namespace Application.Repository
{
    public interface IUserExpenseRepository
    {
        Task AddNewExpenseTypeAsync(NewExpenseType expenseType);
        Task AddNewExpenseAsync(NewExpense expense);
        Task UpdateExpenseAsync(UpdateExpense expense);
        Task DeleteExpenseAsync(DeleteExpense expense);
        Task FetchExpenseAsync(FetchExpense expense);
    }
}

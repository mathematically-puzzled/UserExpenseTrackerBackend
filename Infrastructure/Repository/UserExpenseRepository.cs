using Application.Models;
using Application.Repository;
using Domain;

namespace Infrastructure.Repository
{
    public class UserExpenseRepository : IUserExpenseRepository
    {
        public Task AddNewExpenseAsync(Expenses expense)
        {
            throw new NotImplementedException();
        }

        public Task AddNewExpenseTypeAsync(ExpenseCategories expenseType)
        {
            throw new NotImplementedException();
        }

        public Task DeleteExpenseAsync(Expenses expense)
        {
            throw new NotImplementedException();
        }

        public Task FetchExpenseAsync(FetchExpense expense)
        {
            throw new NotImplementedException();
        }

        public Task UpdateExpenseAsync(Expenses expense)
        {
            throw new NotImplementedException();
        }
    }
}

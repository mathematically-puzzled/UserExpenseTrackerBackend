using Domain;

namespace Application.Repository
{
    public interface IUserExpenseCategoryRepository
    {
        public Task<List<ExpenseCategory>> GetUserExpenseCategoriesAsync(User User);
        public bool AddUserCategoryAsync(ExpenseCategory ExpenseCategory);
        public Task DeleteUserCategoryAsync(ExpenseCategory expenseCategories);
        public Task<ExpenseCategory> GetExpenseByIdASync(Guid Id);
    }
}

using Application.Repository;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class UserExpenseCategoryRepository : IUserExpenseCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserExpenseCategoryRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public bool AddUserCategoryAsync(ExpenseCategory ExpenseCategory)
        {
            try
            {
                bool expenseExists = _context.ExpenseCategoryTable
                                .Any(ue => ue.ExpenseType == ExpenseCategory.ExpenseType);
                if (expenseExists)
                {
                    throw new Exception("Expense category already exists.");
                }
                _context.ExpenseCategoryTable.Add(ExpenseCategory);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteUserCategoryAsync(ExpenseCategory ExpenseType)
        {
            try
            {
                ExpenseCategory ExpenseInDb = await GetExpenseByIdASync(ExpenseType.Id);
                _context.ExpenseCategoryTable.Remove(ExpenseInDb);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ExpenseCategory> GetExpenseByIdASync(Guid Id)
        {
            try
            {
                ExpenseCategory ExpenseInDb = await _context.ExpenseCategoryTable.AsQueryable()
                    .Where(ue => ue.Id == Id)
                    .FirstOrDefaultAsync();
                if (ExpenseInDb == null) throw new Exception("Expense Category does not exist");
                return ExpenseInDb;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ExpenseCategory>> GetUserExpenseCategoriesAsync(User User)
        {
            try
            {
                List<ExpenseCategory> FetchUserExpenses = await _context
                     .ExpenseCategoryTable.AsQueryable()
                     .Where(x => x.User.Id == User.Id || x.FromAdmin == true)
                     .ToListAsync();
                if (FetchUserExpenses.Count > 0)
                {
                    return FetchUserExpenses;
                }
                throw new Exception("No Expense Categories configured in the backend.");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

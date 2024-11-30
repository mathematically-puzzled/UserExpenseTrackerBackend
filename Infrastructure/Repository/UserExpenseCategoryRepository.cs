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

        /// <summary>
        /// Checks if there is an existing expense category then adds it if there isnt
        /// </summary>
        /// <param name="ExpenseCategory"></param>
        /// <returns>
        /// Exception if it exists.
        /// </returns>
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

        /// <summary>
        /// Deletes an Expense Category
        /// </summary>
        /// <param name="ExpenseType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Fetches an expense category by its GUID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// Exception if the GUID Expense does not exist.
        /// </returns>
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

        /// <summary>
        /// Fetches all expense categorie related to User.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
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

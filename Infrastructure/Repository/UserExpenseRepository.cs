using Application.Models.Expense;
using Application.Repository;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    internal class UserExpenseRepository : IUserExpenseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserExpenseRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task AddNewExpenseAsync(Expenses UserExpense)
        {
            try
            {
                _context.ExpensesTable.Add(UserExpense);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteExpenseAsync(Guid UserExpenseId)
        {
            try
            {
                Expenses UserExpense = await GetExpenseByGuidAsync(UserExpenseId); ;
                _context.ExpensesTable.Remove(UserExpense);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Expenses>> FetchUserExpensesAsync(ExpenseRequestModel RequestModel)
        {
            try
            {
                List<Expenses> UserExpenses = await _context.ExpensesTable.AsQueryable()
                    .Where(ex => ex.User.Id == RequestModel.UserId)
                    .ToListAsync();
                if (UserExpenses.Count == 0) throw new Exception("No Expense History Available for requested User");
                if (RequestModel.StartDate != null && RequestModel.EndDate != null)
                {
                    if (RequestModel.StartDate > RequestModel.EndDate) throw new Exception("Incorrect format of DateRange provided");
                    UserExpenses = UserExpenses.Where(ex => ex.ExpenseDate >= RequestModel.StartDate
                    && ex.ExpenseDate <= RequestModel.EndDate).ToList();
                    if (UserExpenses.Count == 0) throw new Exception("No data available in given Time Range");
                }
                if (RequestModel.ExpenseType.Count != 0)
                {
                    UserExpenses = UserExpenses.Where(ex => RequestModel.ExpenseType.Contains(ex.ExpenseType)).ToList();
                    if (UserExpenses.Count == 0) throw new Exception("No data available for requested expense types and given time range");
                }
                return UserExpenses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Expenses> GetExpenseByGuidAsync(Guid UserExpenseId)
        {
            try
            {
                Expenses ExpenseInDb = await _context.ExpensesTable.AsQueryable()
                    .Where(e => e.Id == UserExpenseId)
                    .FirstOrDefaultAsync();
                if (ExpenseInDb != null)
                {
                    return ExpenseInDb;
                }
                throw new Exception("Expense not found in Database");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateExpenseAsync(Expenses UserExpense)
        {
            _context.ExpensesTable.Update(UserExpense);
            await _context.SaveChangesAsync();
        }
    }
}

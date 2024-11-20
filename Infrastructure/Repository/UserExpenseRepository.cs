using Application.Models;
using Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserExpenseRepository : IUserExpenseRepository
    {
        public Task AddNewExpenseAsync(NewExpense expense)
        {
            throw new NotImplementedException();
        }

        public Task AddNewExpenseTypeAsync(NewExpenseType expenseType)
        {
            throw new NotImplementedException();
        }

        public Task DeleteExpenseAsync(DeleteExpense expense)
        {
            throw new NotImplementedException();
        }

        public Task FetchExpenseAsync(FetchExpense expense)
        {
            throw new NotImplementedException();
        }

        public Task UpdateExpenseAsync(UpdateExpense expense)
        {
            throw new NotImplementedException();
        }
    }
}

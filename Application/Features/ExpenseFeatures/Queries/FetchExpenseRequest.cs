using Application.Models.Expense;
using Application.Repository;
using Domain;
using MediatR;

namespace Application.Features.ExpenseFeatures.Queries
{
    public class FetchExpenseRequest : IRequest<List<Expenses>>
    {
        public ExpenseRequestModel ExpenseModel;

        public FetchExpenseRequest(ExpenseRequestModel expenseModel)
        {
            ExpenseModel = expenseModel;
        }
    }

    public class FetchExpenseRequestHandler : IRequestHandler<FetchExpenseRequest, List<Expenses>>
    {
        private readonly IUserExpenseRepository _userExpenseRepository;

        public FetchExpenseRequestHandler(IUserExpenseRepository userExpenseRepository)
        {
            _userExpenseRepository = userExpenseRepository;
        }

        public async Task<List<Expenses>> Handle(FetchExpenseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<Expenses> ExpensesFromDb = await _userExpenseRepository.FetchUserExpensesAsync(request.ExpenseModel);
                return ExpensesFromDb;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

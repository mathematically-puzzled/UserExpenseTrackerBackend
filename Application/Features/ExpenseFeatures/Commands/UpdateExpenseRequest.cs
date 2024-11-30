using Application.Models.Expense;
using Application.Repository;
using Domain;
using MediatR;

namespace Application.Features.ExpenseFeatures.Commands
{
    public class UpdateExpenseRequest : IRequest<bool>
    {
        public UpdateExpense UserExpenseUpdate;

        public UpdateExpenseRequest(UpdateExpense userExpenseUpdate)
        {
            UserExpenseUpdate = userExpenseUpdate;
        }
    }

    public class UpdateExpenseRequestHandler : IRequestHandler<UpdateExpenseRequest, bool>
    {
        private readonly IUserExpenseRepository _userExpenseRepository;

        public UpdateExpenseRequestHandler(IUserExpenseRepository userExpenseRepository)
        {
            _userExpenseRepository = userExpenseRepository;
        }

        public async Task<bool> Handle(UpdateExpenseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Expenses ExpenseInDb = await _userExpenseRepository.GetExpenseByGuidAsync(request.UserExpenseUpdate.Id);
                ExpenseInDb.Description = request.UserExpenseUpdate.Description;
                ExpenseInDb.ExpenseType = request.UserExpenseUpdate.ExpenseType;
                ExpenseInDb.Amount = request.UserExpenseUpdate.Amount;
                ExpenseInDb.ExpenseDate = DateTime.Now;

                await _userExpenseRepository.UpdateExpenseAsync(ExpenseInDb);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

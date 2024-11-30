using Application.Models.Expense;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.ExpenseFeatures.Commands
{
    public class AddNewExpenseRequest : IRequest<bool>
    {
        public NewExpense UserExpense;

        public AddNewExpenseRequest(NewExpense userExpense)
        {
            UserExpense = userExpense;
        }
    }

    public class AddNewExpenseRequestHandler : IRequestHandler<AddNewExpenseRequest, bool>
    {
        private readonly IUserExpenseRepository _userExpRepo;
        private readonly IMapper _mapper;
        private readonly IUserAuthRepository _userAuthRepo;

        public AddNewExpenseRequestHandler(IUserExpenseRepository userExpRepo, IMapper mapper, IUserAuthRepository userAuthRepo)
        {
            _userExpRepo = userExpRepo;
            _mapper = mapper;
            _userAuthRepo = userAuthRepo;
        }

        public async Task<bool> Handle(AddNewExpenseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User UserInDb = await _userAuthRepo.GetUserByIdAsync(request.UserExpense.UserId);
                Expenses UserExpenses = _mapper.Map<Expenses>(request.UserExpense);
                UserExpenses.User = UserInDb;
                UserExpenses.ExpenseDate = DateTime.Now;

                await _userExpRepo.AddNewExpenseAsync(UserExpenses);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

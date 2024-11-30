using Application.Models.ExpenseCategories;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.ExpenseCategoryFeatures.Commands
{
    public class AddUserExpenseCategoryRequest : IRequest<bool>
    {
        public AddExpenseCategory ExpenseBody { get; set; }

        public AddUserExpenseCategoryRequest(AddExpenseCategory expenseBody)
        {
            ExpenseBody = expenseBody;
        }
    }

    public class AddUserExpenseCategoryRequestHandler : IRequestHandler<AddUserExpenseCategoryRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUserExpenseCategoryRepository _UserExpCtgRepo;
        private readonly IUserAuthRepository _UserAuthRepo;

        public AddUserExpenseCategoryRequestHandler(IUserExpenseCategoryRepository userExpCtgRepo, IMapper mapper, IUserAuthRepository userAuthRepo)
        {
            _UserExpCtgRepo = userExpCtgRepo;
            _mapper = mapper;
            _UserAuthRepo = userAuthRepo;
        }

        public async Task<bool> Handle(AddUserExpenseCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User UserInDb = await _UserAuthRepo.GetUserByIdAsync(request.ExpenseBody.Id);
                ExpenseCategory Category = new()
                {
                    ExpenseType = request.ExpenseBody.ExpenseType,
                    FromAdmin = false,
                    User = UserInDb
                };
                ExpenseCategory ExpCtg = _mapper.Map<ExpenseCategory>(Category);
                return _UserExpCtgRepo.AddUserCategoryAsync(ExpCtg);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Application.Models.ExpenseCategories;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.ExpenseCategoryFeatures.Queries
{
    public class FetchUserExpenseCategoriesRequest : IRequest<List<UserExpenseCategory>>
    {
        public Guid Id { get; set; }

        public FetchUserExpenseCategoriesRequest(Guid id)
        {
            Id = id;
        }
    }

    public class FetchUserExpenseCategoriesRequestHandler : IRequestHandler<FetchUserExpenseCategoriesRequest, List<UserExpenseCategory>>
    {
        private readonly IMapper _mapper;
        private readonly IUserExpenseCategoryRepository _userExpCtgRepo;
        private readonly IUserAuthRepository _userAuthRepo;

        public FetchUserExpenseCategoriesRequestHandler(IUserExpenseCategoryRepository userExpCtgRepo, IMapper mapper, IUserAuthRepository userAuthRepo)
        {
            _userExpCtgRepo = userExpCtgRepo;
            _mapper = mapper;
            _userAuthRepo = userAuthRepo;
        }

        public async Task<List<UserExpenseCategory>> Handle(FetchUserExpenseCategoriesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User User = await _userAuthRepo.GetUserByIdAsync(request.Id);
                List<ExpenseCategory> ExpCtgFromDb = await _userExpCtgRepo.GetUserExpenseCategoriesAsync(User);
                List<UserExpenseCategory> UserExpCtg = new();
                foreach (ExpenseCategory ExpCtg in ExpCtgFromDb)
                {
                    UserExpenseCategory temp = _mapper.Map<UserExpenseCategory>(ExpCtg);
                    UserExpCtg.Add(temp);
                }
                return UserExpCtg;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

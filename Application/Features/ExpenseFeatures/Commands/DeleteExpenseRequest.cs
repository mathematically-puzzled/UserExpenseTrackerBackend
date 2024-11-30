using Application.Repository;
using MediatR;

namespace Application.Features.ExpenseFeatures.Commands
{
    public class DeleteExpenseRequest : IRequest<bool>
    {
        public Guid Id;

        public DeleteExpenseRequest(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteExpenseRequestHandler : IRequestHandler<DeleteExpenseRequest, bool>
    {
        private readonly IUserExpenseRepository _UserExpRepo;

        public DeleteExpenseRequestHandler(IUserExpenseRepository userExpRepo)
        {
            _UserExpRepo = userExpRepo;
        }

        public async Task<bool> Handle(DeleteExpenseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _UserExpRepo.DeleteExpenseAsync(request.Id);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Application.Repository;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseCategoryFeatures.Commands
{
    public class DeleteUserExpenseCategoryRequest : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteUserExpenseCategoryRequest(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteUserExpenseCategoryRequestHandler : IRequestHandler<DeleteUserExpenseCategoryRequest, bool>
    {
        private readonly IUserExpenseCategoryRepository _userExpCtgRepo;

        public DeleteUserExpenseCategoryRequestHandler(IUserExpenseCategoryRepository userExpCtgRepo)
        {
            _userExpCtgRepo = userExpCtgRepo;
        }

        public async Task<bool> Handle(DeleteUserExpenseCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ExpenseCategory User = new()
                {
                    Id = request.Id
                };
                await _userExpCtgRepo.DeleteUserCategoryAsync(User);
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

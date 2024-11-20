using Application.Repository;
using MediatR;

namespace Application.Features.User_Features.Commands
{
    public class DeleteUserRequest : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteUserRequest(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, bool>
    {
        private readonly IUserAuthRepository _userRepo;

        public DeleteUserRequestHandler(IUserAuthRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            bool isSuccessful = await _userRepo.DeletUserAsync(request.Id);
            if (isSuccessful) return true;
            return false;
        }
    }
}

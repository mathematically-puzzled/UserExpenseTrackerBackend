using Application.Repository;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    public class DeleteUserRequest : IRequest
    {
        public Guid Id { get; set; }

        public DeleteUserRequest(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest>
    {
        private readonly IUserAuthRepository _userRepo;

        public DeleteUserRequestHandler(IUserAuthRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepo.DeletUserAsync(request.Id);
                return Unit.Value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

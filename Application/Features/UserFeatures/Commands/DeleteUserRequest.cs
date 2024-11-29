using Application.Repository;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    /// <summary>
    /// Method to recieve Request and Initialize constructor.
    /// </summary>
    public class DeleteUserRequest : IRequest
    {
        public Guid Id { get; set; }

        public DeleteUserRequest(Guid id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Deletes the User by referring the UserAuthRepo
    /// </summary>
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

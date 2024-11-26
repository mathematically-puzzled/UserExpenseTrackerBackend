using Application.Repository;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    /// <summary>
    /// Method to recieve Request and Initialize constructor.
    /// </summary>
    public class DeleteUserRequest : IRequest<bool>
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

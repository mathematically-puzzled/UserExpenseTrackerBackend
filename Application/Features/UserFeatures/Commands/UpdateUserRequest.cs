using Application.Models.Users;
using Application.Pipeline_Behaviour.Contracts;
using Application.Repository;
using Domain;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    /// <summary>
    /// Method to recieve Request and Initialize constructor.
    /// </summary>
    public class UpdateUserRequest : IRequest<bool>, IValidate
    {
        public UpdateUser UserRequest { get; set; }

        public UpdateUserRequest(UpdateUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

    /// <summary>
    /// No IMapper, Manual Mapping since it replaces Values that are empty 
    /// with empty values.
    /// </summary>
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, bool>
    {
        private readonly IUserAuthRepository _userRepo;

        public UpdateUserRequestHandler(IUserAuthRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            User User = await _userRepo.GetUserByIdAsync(request.UserRequest.Id);
            
            User.Name = request.UserRequest.Name;
            User.Currency = request.UserRequest.Currency;
            User.BankBalance = request.UserRequest.BankBalance;
            User.MobileNumber = request.UserRequest.MobileNumber;

            await _userRepo.UpdateUserAsync(User);
            return true;
        }
    }
}

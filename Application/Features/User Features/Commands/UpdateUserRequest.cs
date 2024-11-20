using Application.Models;
using Application.Pipeline_Behaviour.Contracts;
using Application.Repository;
using Domain;
using MediatR;

namespace Application.Features.User_Features.Commands
{
    public class UpdateUserRequest : IRequest<bool>, IValidate
    {
        public UpdateUser UserRequest { get; set; }

        public UpdateUserRequest(UpdateUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

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
            if (User != null)
            {
                User.Name = request.UserRequest.Name;
                User.Currency = request.UserRequest.Currency;
                User.BankBalance = request.UserRequest.BankBalance;
                User.MobileNumber = request.UserRequest.MobileNumber;

                await _userRepo.UpdateUserAsync(User);
                return true;
            }
            return false;
        }
    }
}

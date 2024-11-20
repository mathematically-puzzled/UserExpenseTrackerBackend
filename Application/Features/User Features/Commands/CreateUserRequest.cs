using Application.Models;
using Application.Pipeline_Behaviour.Contracts;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.User_Features.Commands
{
    public class CreateUserRequest : IRequest<bool>, IValidate
    {
        public NewUser UserRequest { get; set; }

        public CreateUserRequest(NewUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, bool>
    {
        private readonly IUserAuthRepository _userAuthRepo;
        private readonly IMapper _mapper;

        public CreateUserRequestHandler(IUserAuthRepository userAuthRepo, IMapper mapper)
        {
            _userAuthRepo = userAuthRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            User User = _mapper.Map<User>(request.UserRequest);
            bool isUserPresent = await _userAuthRepo.IsUserPresentAsync(User);
            if (!isUserPresent)
            {
                User.JoinedDate = DateTime.Now;
                await _userAuthRepo.RegisterUserAsync(User);
                return true;
            }
            return false;
        }
    }
}

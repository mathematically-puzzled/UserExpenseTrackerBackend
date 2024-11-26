using Application.Models.Users;
using Application.Pipeline_Behaviour.Contracts;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    /// <summary>
    /// Method to recieve Request and Initialize constructor.
    /// </summary>
    public class CreateUserRequest : IRequest<bool>, IValidate
    {
        public NewUser UserRequest { get; set; }

        public CreateUserRequest(NewUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

    /// <summary>
    /// Maps UserRequest Param over User inside Db to create new Record.
    /// DateTime rights are not given for the User to be updated since it can
    /// be manipulated. Has IValidate interface,Pipeline Validate behaviour is expected here.
    /// </summary>
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
            bool IsUserRegistered = await _userAuthRepo.RegisterUserAsync(User);
            if (IsUserRegistered) return true;
            return false;
        }
    }
}

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
    public class LoginUserRequest : IRequest<UserDto>, IValidate
    {
        public LoginUser UserCredentials { get; set; }

        public LoginUserRequest(LoginUser userCredentials)
        {
            UserCredentials = userCredentials;
        }
    }

    /// <summary>
    /// Maps UserCredentials over to User and calls the UserLoginAsyncToken that recieves the User
    /// parameter if the Credentials match. Generates the Token if User param is not null.
    /// </summary>
    public class LoginRequestHandler : IRequestHandler<LoginUserRequest, UserDto>
    {
        private readonly IUserAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public LoginRequestHandler(IUserAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            User UserInDb = await _userRepo.UserLoginAsync(request.UserCredentials.UserEmail, request.UserCredentials.Password);
            if (UserInDb != null)
            {
                UserDto UserDTO = _mapper.Map<UserDto>(UserInDb);
                string BearerToken = await _userRepo.GenerateJWTToken(UserInDb);

                UserDTO.PasswordLength = UserInDb.Password.Length;
                UserDTO.BearerToken = BearerToken;
                return UserDTO;
            }
            return null;
        }
    }
}

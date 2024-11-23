using Application.Models.Users;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    public class LoginUserRequest : IRequest<UserDto>
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public LoginUserRequest(string userEmail, string password)
        {
            UserEmail = userEmail;
            Password = password;
        }
    }

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
            User UserInDb = await _userRepo.UserLoginAsync(request.UserEmail, request.Password);
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

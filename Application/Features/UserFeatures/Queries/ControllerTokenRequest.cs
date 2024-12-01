using Application.Repository;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries
{
    public class ControllerTokenRequest : IRequest<string>
    {
        public User User;

        public ControllerTokenRequest(User user)
        {
            User = user;
        }
    }

    public class ControllerTokenRequestHander : IRequestHandler<ControllerTokenRequest, string>
    {
        private readonly IUserAuthRepository _userAuthRepo;

        public ControllerTokenRequestHander(IUserAuthRepository userAuthRepo)
        {
            _userAuthRepo = userAuthRepo;
        }

        public async Task<string> Handle(ControllerTokenRequest request, CancellationToken cancellationToken)
        {
            return await _userAuthRepo.GenerateJWTToken(request.User);
        }
    }
}

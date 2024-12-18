﻿using Application.Models.Users;
using Application.Pipeline_Behaviour.Contracts;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserFeatures.Commands
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
            try
            {
                User User = _mapper.Map<User>(request.UserRequest);
                bool IsUserRegistered = await _userAuthRepo.RegisterUserAsync(User);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

﻿using Application.Models.Users;
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
    public class ForgotUserPasswordRequest : IRequest<bool>, IValidate
    {
        public ForgotUser UserCredentials { get; set; }

        public ForgotUserPasswordRequest(ForgotUser userCredentials)
        {
            UserCredentials = userCredentials;
        }
    }

    /// <summary>
    /// Maps UserCredentials over User. Has IValidate interface,Pipeline Validate behaviour 
    /// is expected here.
    /// </summary>
    public class ForgotUserPasswordRequestHandler : IRequestHandler<ForgotUserPasswordRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUserAuthRepository _userRepo;

        public ForgotUserPasswordRequestHandler(IUserAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ForgotUserPasswordRequest request, CancellationToken cancellationToken)
        {
            User UserInDb = _mapper.Map<User>(request.UserCredentials);
            try
            {
                bool isActionSuccessfull = await _userRepo.ForgotPasswordAsync(UserInDb);
                if (isActionSuccessfull) return true;
                else return false;
                throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

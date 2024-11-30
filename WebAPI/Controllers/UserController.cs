using Application.Features.UserFeatures.Commands;
using Application.Models.Users;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ResponseModel;
using WebAPI.ResponseModel.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "UserController")]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        GenericResponseMethod responseGenerator = new();

        public UserController(ISender mediatrSender, GenericResponseMethod responseGenerator)
        {
            _mediatrSender = mediatrSender;
            this.responseGenerator = responseGenerator;
        }

        /// <summary>
        /// Login function for the User.
        /// </summary>
        /// <param name="Credentials"></param>
        /// <returns>
        /// Updated User DTO with Auth Token valid for 120 Minutes
        /// </returns>
        /// <exception cref="Exception">
        /// Validation Exception for Credentials
        /// </exception>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> LoginUser(LoginUser Credentials)
        {
            try
            {
                object UserDto = await _mediatrSender.Send(new LoginUserRequest(Credentials));
                return responseGenerator.GenerateResponseMethod(200, "User logged in successfully", UserDto);
            }
            catch (ValidationException ex)
            {
                return responseGenerator.GenerateResponseMethod(400, "One or more validation error(s) occured", ex.Message.Split(Environment.NewLine));
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        /// <summary>
        /// Password reset function for the User.
        /// </summary>
        /// <param name="UserCredentials"></param>
        /// <returns>
        /// Status code. 200 for Updated Records; 404 for User Credentials not found; 500 Validation Exceptions.
        /// </returns>
        /// <exception cref="ArgumentException"/>
        [HttpPost("forgor")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> ForgotUserPassword(ForgotUser UserCredentials)
        {
            try
            {
                bool isActionSuccessful = await _mediatrSender.Send(new ForgotUserPasswordRequest(UserCredentials));
                if (isActionSuccessful) return responseGenerator.GenerateResponseMethod(200, "User credentials updated successfully", null);
                return responseGenerator.GenerateResponseMethod(404, "User credentials does not exists", null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        /// <summary>
        /// Register user function for the User.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>
        /// Status code. 200 for User Created; 500 for User Already Exists/Validation Exceptions.
        /// </returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="Exception"/>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> RegisterNewUser([FromBody] NewUser newUser)
        {
            try
            {
                await _mediatrSender.Send(new CreateUserRequest(newUser));
                return responseGenerator.GenerateResponseMethod(200, "User Created Successfully", null);
            }
            catch (ValidationException ex)
            {
                return responseGenerator.GenerateResponseMethod(400, "One or more validation error(s) occured", ex.Message.Split(Environment.NewLine));
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        /// <summary>
        /// Update record function for the User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// Status code. 200 for Record Updated; 404 for User not found; 500 for Validation Exceptions.
        /// </returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="Exception"/>
        [HttpPut("update")]
        [Authorize]
        public async Task<GenericResponseModel> UpdateUser([FromBody] UpdateUser user)
        {
            try
            {
                await _mediatrSender.Send(new UpdateUserRequest(user));
                return responseGenerator.GenerateResponseMethod(200, "User fields updated", null);
            }
            catch (ValidationException ex)
            {
                return responseGenerator.GenerateResponseMethod(400, "One or more validation error(s) occured", ex.Message.Split(Environment.NewLine));
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        /// <summary>
        /// Remove record function for the User.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// Status code. 200 for User Deleted; 404 for User not found.
        /// </returns>
        [HttpDelete("delete")]
        [Authorize]
        public async Task<GenericResponseModel> RemoveUser(Guid Id)
        {
            try
            {
                await _mediatrSender.Send(new DeleteUserRequest(Id));
                return responseGenerator.GenerateResponseMethod(200, "User deleted", null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(404, ex.Message, null);
            }
        }
    }
}

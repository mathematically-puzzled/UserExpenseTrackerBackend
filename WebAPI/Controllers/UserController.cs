using Application.Features.UserFeatures.Commands;
using Application.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ResponseModel;
using WebAPI.ResponseModel.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        GenericResponseMethod responseGenerator = new();

        public UserController(ISender mediatrSender, GenericResponseMethod responseGenerator)
        {
            _mediatrSender = mediatrSender;
            this.responseGenerator = responseGenerator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> LoginUser(LoginUser Credentials)
        {
            try
            {
                object UserDto = await _mediatrSender.Send(new LoginUserRequest(Credentials));
                if (UserDto != null) return responseGenerator.GenerateResponseMethod(200, "User logged in successfully", UserDto);
                throw new Exception();
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, "One or more validation error(s) occured", ex.Message.Split(Environment.NewLine));
            }
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> RegisterNewUser([FromBody] NewUser newUser)
        {
            try
            {
                bool isSuccesful = await _mediatrSender.Send(new CreateUserRequest(newUser));
                if (isSuccesful) return responseGenerator.GenerateResponseMethod(200, "User Created Successfully", null);
                else throw new ArgumentException("User already exists");
                throw new Exception();
            }
            catch (ArgumentException ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, "One or more validation error(s) occured", ex.Message.Split(Environment.NewLine));
            }
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<GenericResponseModel> UpdateUser([FromBody] UpdateUser user)
        {
            try
            {
                bool isSuccessful = await _mediatrSender.Send(new UpdateUserRequest(user));
                if (isSuccessful) return responseGenerator.GenerateResponseMethod(200, "User fields updated", null);
                else throw new ArgumentException("User does not exist");
                throw new Exception();
            }
            catch (ArgumentException ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, "One or more validation error(s) occured.", ex.Message.Split(Environment.NewLine));
            }
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<GenericResponseModel> RemoveUser(Guid Id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeleteUserRequest(Id));
            if (isSuccessful) return responseGenerator.GenerateResponseMethod(200, "User deleted", null);
            return responseGenerator.GenerateResponseMethod(500, "User does not exists", null);
        }
    }
}

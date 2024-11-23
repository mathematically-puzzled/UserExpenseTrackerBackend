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
        public async Task<GenericResponseModel> LoginUser(string UserEmail, string Password)
        {
            object UserDto = await _mediatrSender.Send(new LoginUserRequest(UserEmail, Password));
            if (UserDto != null) return responseGenerator.GenerateResponseMethod(200, "User logged in successfully", UserDto);
            return responseGenerator.GenerateResponseMethod(500, "Incorrect Username or Password", null);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<GenericResponseModel> RegisterNewUser([FromBody] NewUser newUser)
        {
            bool isSuccesful = await _mediatrSender.Send(new CreateUserRequest(newUser));
            if (isSuccesful) return responseGenerator.GenerateResponseMethod(200, "User Created Successfully", null);
            return responseGenerator.GenerateResponseMethod(500, "User already exists", null);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<GenericResponseModel> UpdateUser([FromBody] UpdateUser user)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdateUserRequest(user));
            if (isSuccessful) return responseGenerator.GenerateResponseMethod(200, "User fields updated", null);
            return responseGenerator.GenerateResponseMethod(500, "User does not exists", null);
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

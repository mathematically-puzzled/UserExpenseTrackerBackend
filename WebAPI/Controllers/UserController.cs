using Application.Features.User_Features.Commands;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Response_Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public UserController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        public async Task<GenericResponse> RegisterNewUser([FromBody] NewUser newUser)
        {
            GenericResponse response = new();
            bool isSuccesful = await _mediatrSender.Send(new CreateUserRequest(newUser));
            if (isSuccesful)
            {
                response.Message = "User created successfully";
                response.StatusCode = 200;
                response.Data = null;

                return response;
            }
            response.Message = "User already exists";
            response.StatusCode = 500;
            response.Data = null;

            return response;
        }

        [HttpPut("update")]
        public async Task<GenericResponse> UpdateUser([FromBody] UpdateUser user)
        {
            GenericResponse response = new();
            bool isSuccessful = await _mediatrSender.Send(new UpdateUserRequest(user));
            if (isSuccessful)
            {
                response.Message = "User fields updated";
                response.StatusCode = 200;
                response.Data = null;

                return response;
            }
            response.Message = "User does not exist";
            response.StatusCode = 500;
            response.Data = null;

            return response;
        }

        [HttpDelete("delete")]
        public async Task<GenericResponse> RemoveUser(Guid Id)
        {
            GenericResponse response = new();
            bool isSuccessful = await _mediatrSender.Send(new DeleteUserRequest(Id));
            if (isSuccessful)
            {
                response.Message = "User deleted";
                response.StatusCode = 200;
                response.Data = null;

                return response;
            }
            response.Message = "User does not exist";
            response.StatusCode = 500;
            response.Data = null;

            return response;
        }
    }
}

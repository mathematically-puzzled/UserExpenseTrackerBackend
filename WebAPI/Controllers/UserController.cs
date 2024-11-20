using Application.Features.User_Features.Commands;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RegisterNewUser([FromBody] NewUser newUser)
        {
            bool isSuccesful = await _mediatrSender.Send(new CreateUserRequest(newUser));
            if (isSuccesful) return Ok("User created successfully");
            return BadRequest("User already Exists");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdateUserRequest(user));
            if (isSuccessful) return Ok("User fields updated");
            return BadRequest("User does not exist");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveUser(Guid Id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeleteUserRequest(Id));
            if (isSuccessful) return Ok("User Deleted");
            return BadRequest("User does not exist");
        }
    }
}

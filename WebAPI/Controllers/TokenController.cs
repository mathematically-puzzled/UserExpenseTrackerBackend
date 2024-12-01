using Application.Features.UserFeatures.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ResponseModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "TokenController")]
    public class TokenController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public TokenController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("Token")]
        [AllowAnonymous]
        public async Task<string> GetSwaggerToken(User User)
        {
            return await _mediatrSender.Send(new ControllerTokenRequest(User));
        }
    }
}

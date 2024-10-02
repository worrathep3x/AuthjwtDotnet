using Application.DTO.Response;
using Application.Handler.UserAuthen.Queries;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserAuthenApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserAuthenController : ApiControllerBase
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        [HttpPost]
        [Route("/auth/Login")]
        [AllowAnonymous]
        public async Task<Response<GetAdminAuthenResponse>> AccessToken(LoginAccessTokenQuery request)
        {
            var response = await Mediator.Send(request);
            return response;
        }

        /// <summary>
        /// Verify
        /// </summary>
        [HttpGet]
        [Route("/auth/Verify")]
        public async Task<Response<GetAdminAuthenResponse>> VerifyToken(ISender sender)
        {
            string Httptoken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var response = await sender.Send(new VerifyTokenQuery(Httptoken));
            return response;
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [HttpGet]
        [Route("/UserAcc/RefreshToken")]
        public async Task<Response<GetUserAuthenResponseDTO>> RefreshToken(ISender sender)
        {
            HttpContext Httptoken = HttpContext;
            var response = await sender.Send(new RefreshTokenQuery(Httptoken));
            return response;
        }

    }
}

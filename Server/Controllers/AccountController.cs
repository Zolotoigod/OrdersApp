using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersApp.Server.Autentification;
using OrdersApp.Shared.DTO;

namespace OrdersApp.Server.Controllers
{
    [Route("/api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccauntService accauntService;

        public AccountController(IAccauntService accauntService)
        {
            this.accauntService = accauntService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Login([FromBody] LoginRequest request) 
        {
            var jwtAuthManger = new JwtService(accauntService);
            var session = await jwtAuthManger.CreateToken(request.Login, request.Password);

            return session is null ? Unauthorized() : session;
        }
    }
}

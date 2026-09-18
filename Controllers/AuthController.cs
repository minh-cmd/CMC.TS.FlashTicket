using CMC.TS.FT.Api.DTO.Auth;
using CMC.TS.FT.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMC.TS.FT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController (AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            string? token = await _authService.Login(login);
            if (token == null) 
            {
                return BadRequest();
            }
            return Ok(token);
        }
        
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegisterDTO registerDTO)
        {
            bool isSuccess = await _authService.Register(registerDTO);
            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

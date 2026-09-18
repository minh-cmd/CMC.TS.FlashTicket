using CMC.TS.FT.Api.DTO.Role;
using CMC.TS.FT.Api.DTO.User;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.HelperClass;
using CMC.TS.FT.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMC.TS.FT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        { 
             _userService = userService;
        }

        [Authorize(Roles = "users.read.all")]
        [HttpGet]
        public async Task<IActionResult> DisplayAllUser()
        {
            List<DisplayUserDTO>? userDTOs = await _userService.DisplayAllUser();
            if (userDTOs == null)
                return BadRequest();
            return Ok(userDTOs);
        }

        [HttpPost]
        [Authorize(Roles = "users.create.all")]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            bool isSuccess = await _userService.CreateUser(createUserDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "users.update.all")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateProfileDTO profileDTO)
        {
            bool isSuccess = await _userService.UpdateProfile(id, profileDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("me")]
        [Authorize(Roles = "users.read.own")]
        public async Task<IActionResult> DisplayMe()
        {
            Guid id = JwtExtract.ExtractUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User? user = await _userService.DisplayProfile(id);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpDelete("me")]
        [Authorize(Roles = "users.delete.own")]
        public async Task<IActionResult> DeleteMe() 
        {
            Guid id = JwtExtract.ExtractUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool isSuccess = await _userService.DeleteUser(id);
            if (isSuccess)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("me")]
        [Authorize(Roles = "users.update.own")]
        public async Task<IActionResult> UpdateMe(UpdateProfileDTO profileDTO)
        {
            Guid id = JwtExtract.ExtractUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool isSuccess = await _userService.UpdateProfile(id, profileDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

    }
}

using CMC.TS.FT.Api.DTO.Role;
using CMC.TS.FT.Api.DTO.User;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> DisplayAllUser()
        {
            List<DisplayUserDTO>? userDTOs = await _userService.DisplayAllUser();
            if (userDTOs == null)
                return BadRequest();
            return Ok(userDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            bool isSuccess = await _userService.CreateUser(createUserDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateProfileDTO profileDTO)
        {
            bool isSuccess = await _userService.UpdateProfile(id, profileDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("me")]
        public async Task<IActionResult> DisplayMe(Guid id)
        {
            User? user = await _userService.DisplayProfile(id);
            if(user  == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMe(Guid id) 
        { 
            bool isSuccess = await _userService.DeleteUser(id);
            if (isSuccess)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMe(Guid id, UpdateProfileDTO profileDTO)
        {
            bool isSuccess = await _userService.UpdateProfile(id, profileDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

    }
}

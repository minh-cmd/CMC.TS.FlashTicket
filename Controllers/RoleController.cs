using CMC.TS.FT.Api.DTO.Role;
using CMC.TS.FT.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMC.TS.FT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }
        
        [HttpPut]
        public async Task<IActionResult> AssignPermissionToRoleDTO(AssignPermissionToRoleDTO permissionToRoleDTO)
        {
            bool isSuccess = await _roleService.SyncRolePermission(permissionToRoleDTO);
            if (isSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}

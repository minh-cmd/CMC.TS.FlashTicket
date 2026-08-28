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
        
        [HttpPut("{roleId:guid}/permission")]
        public async Task<IActionResult> AssignPermissionToRoleDTO([FromRoute] Guid roleId, [FromBody] List<Guid>? permissionIds)
        {
            
            AssignPermissionToRoleDTO permissionToRoleDTO = new AssignPermissionToRoleDTO
            {
                RoleId = roleId,
                PermissionIds = permissionIds
            };
            
            bool isSuccess = await _roleService.SyncRolePermission(permissionToRoleDTO);
            if (isSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllRole()
        {
            List<DisplayRoleDTO>? displayRoles = await _roleService.DisplayAllRole();
            return Ok(displayRoles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            bool isSuccess = await _roleService.DeleteRole(id);
            if(isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRole(CreateRoleDTO roleDTO)
        {
            bool isSuccess = await _roleService.CreateNewRole(roleDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleDTO roleDTO)
        {
            bool isSuccess = await _roleService.UpdateRole(roleDTO);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }
    }
}

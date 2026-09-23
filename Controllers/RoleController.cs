using CMC.TS.FT.Api.DTO.Role;
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
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }
        
        [HttpPut("{roleId:guid}/permission")]
        [Authorize(Roles = "roles.assign_permissions.all")]
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
        [Authorize(Roles = "roles.read.all")]
        public async Task<IActionResult> DisplayAllRole()
        {
            List<Role>? displayRoles = await _roleService.DisplayAllRole();
            return Ok(displayRoles);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "roles.delete.all")]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            Guid updateBy = JwtExtract.ExtractUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool isSuccess = await _roleService.DeleteRole(id, updateBy);
            if(isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "roles.create.all")]
        public async Task<IActionResult> CreateNewRole(CreateRoleDTO roleDTO)
        {
            Guid createBy = JwtExtract.ExtractUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool isSuccess = await _roleService.CreateNewRole(roleDTO, createBy);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "roles.update.all")]
        public async Task<IActionResult> UpdateRole(Guid id, UpdateRoleDTO roleDTO)
        {

            Guid updateBy = JwtExtract.ExtractUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool isSuccess = await _roleService.UpdateRole(id, roleDTO, updateBy);
            if (isSuccess)
                return Ok();
            else
                return BadRequest();
        }
    }
}

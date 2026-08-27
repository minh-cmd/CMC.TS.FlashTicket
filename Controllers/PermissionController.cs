using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMC.TS.FT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpGet]
        public async Task<IActionResult> DisplayPermission()
        {
            List<Permission>? a = await _permissionService.DisplayAllPermission();
            if (a != null)
            {
                return Ok(a);
            }
            return BadRequest();
        }
    }
}

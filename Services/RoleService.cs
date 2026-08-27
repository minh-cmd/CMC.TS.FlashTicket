using CMC.TS.FT.Api.DTO.Role;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Repositories.GenericRepository;
using CMC.TS.FT.Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CMC.TS.FT.Api.Services
{
    public class RoleService
    {
        private readonly ILogger _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleService(ILogger logger, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        //chức năng mang tính chất full replacement. cập nhật lại toàn bộ permission của một role theo request mới nhất từ phía client
        public async Task<bool> SyncRolePermission(AssignPermissionToRoleDTO rolePermission)
        {
            try
            {
                _logger.LogInformation("Assign permission to role operation start");
                if(rolePermission.PermissionIds == null)
                {
                    _logger.LogError("permission Id can't be null");
                    return false;
                }

                //xoá hết permission cũ của role.
                bool isSuccess = await _roleRepository.DeleteRolePermissionByRoleId(rolePermission.RoleId);
                if(isSuccess == false)
                {
                    _logger.LogInformation("Can't delete permission");
                }

                if (rolePermission.PermissionIds.Count != 0)
                {
                    //Thêm các permission mới vào role
                    List<RolePermission> thisisnew = rolePermission.PermissionIds.Select(pi => new RolePermission
                    {
                        RoleId = rolePermission.RoleId,
                        PermissionId = pi,
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow,
                        CreateBy = Guid.Empty,
                        UpdateBy = Guid.Empty,
                    }).ToList();
                    await _roleRepository.AddManyPermissionToRoleId(thisisnew);
                }
                return true;
                /*//Trường hợp người dùng để trống (xoá tất cả permission của role)
                if (rolePermission.PermissionIds.Count == 0) 
                {
                    _logger.LogInformation("permission Id is empty");
                    bool isSuccess = await _roleRepository.DeleteRolePermissionByRoleId(rolePermission.RoleId);
                    if (isSuccess)
                    { 
                        _logger.LogInformation("Delete all permission from this role {RoleId}",rolePermission.RoleId);
                        return true;
                    }
                    else
                    {
                        _logger.LogInformation("Delete operation affect 0 row {RoleId}", rolePermission.RoleId);
                        return false;
                    }
                }

                //Trường hợp không để trống
                //b1: xoá những permission cũ trong không có trong List permissionIds của DTO (nếu trùng thì giữ nguyên)
                //lấy ra permission cũ không có trong List permissionIds của DTO như thế nào?
                //b1.1: Phải so sánh id cũ với List id mới, lấy ra id không có

                //b2: thêm những cái permission mới vào database*/
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Assign permission to role operation failed");
                return false;
            }
        }
    }
}

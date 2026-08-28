using CMC.TS.FT.Api.DTO.Role;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Repositories.GenericRepository;
using CMC.TS.FT.Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CMC.TS.FT.Api.Services
{
    public class RoleService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleService(ILogger<RoleService> logger, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        //chức năng mang tính chất full replacement. cập nhật lại toàn bộ permission của một role theo request mới nhất từ phía client
        public async Task<bool> SyncRolePermission(AssignPermissionToRoleDTO rolePermission)
        {
            try
            {
                //Log bắt đầu
                _logger.LogInformation("SyncRolePermission operation start");

                //PermissionIds không được phép null
                if(rolePermission.PermissionIds == null)
                {
                    _logger.LogError("permission Id can't be null");
                    return false;
                }
                //nếu PermissionIds hoàn toàn trống. tức người dùng chỉ muốn xoá. Thì xoá và thoát.
                //xoá hết permission cũ của role.
                if(rolePermission.PermissionIds.Count == 0)
                {
                    bool isSuccess = await _roleRepository.DeleteRolePermissionByRoleId(rolePermission.RoleId);
                    if (isSuccess == false)
                    {
                        _logger.LogInformation("Can't delete permission");
                        return false;
                    }
                    else if (isSuccess == true)
                    {
                        _logger.LogInformation("delete permission success");
                        return true;
                    }
                }

                //nếu permissionIds không hoàn toàn trống. Thì vẫn xoá hết permissionId trong database và thêm cái mới vào.
                if (rolePermission.PermissionIds.Count != 0)
                {
                    //xoá các permissionId cũ
                    bool isSuccess = await _roleRepository.DeleteRolePermissionByRoleId(rolePermission.RoleId);
                    if (isSuccess == false)
                    {
                        _logger.LogInformation("Can't delete permission");
                    }
                    else if (isSuccess == true)
                    {
                        _logger.LogInformation("delete permission success");
                    }

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
        public async Task<bool> CreateNewRole(CreateRoleDTO createRole)
        {
            try
            {
                _logger.LogInformation("create new role operation start");
                Role role = new Role 
                { 
                    RoleId = Guid.NewGuid(),
                    RoleName = createRole.RoleName,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = null,
                    CreateBy = Guid.Empty,
                    UpdateBy = Guid.Empty
                };

                bool isSuccess = await _roleRepository.Create(role);
                if(isSuccess == true)
                {
                    _logger.LogInformation("create new role success");
                    return true;
                }
                else
                {
                    _logger.LogInformation("create new role failed");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "create new role operation failed");
                return false;
            }
        }
        public async Task<bool> DeleteRole(Guid roleId)
        {
            try
            {
                _logger.LogInformation("delete role operation start");
                bool isSuccess = await _roleRepository.Delete(roleId);
                if (isSuccess == true)
                {
                    _logger.LogInformation("delete role success");
                    return true;
                }
                else
                {
                    _logger.LogInformation("delete role failed");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "delete role operation failed");
                return false;
            }
        }
        public async Task<List<DisplayRoleDTO>?> DisplayAllRole()
        {
            try
            {
                _logger.LogInformation("display all role operation start");
                List<Role>? roles = await _roleRepository.GetAll();

                if(roles == null || roles.Count == 0)
                {
                    _logger.LogError("roles are null");
                    return null;
                }

                List<DisplayRoleDTO> displayRoleDTOs = roles.Select(r => new DisplayRoleDTO
                {
                    RoleName = r.RoleName,
                }).ToList();
                return displayRoleDTOs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "display role operation failed");
                return null;
            }
        }
        public async Task<bool> UpdateRole(UpdateRoleDTO updateRoleDTO)
        {
            try
            {
                _logger.LogInformation("update role operation start");
                Role? role = await _roleRepository.GetById(updateRoleDTO.RoleId);
                if (role == null)
                {
                    _logger.LogError("there is no role with {id}", updateRoleDTO.RoleId);
                    return false;
                }

                role.RoleName = updateRoleDTO.RoleName;
                role.UpdateBy = Guid.Empty;
                role.UpdateAt = DateTime.UtcNow;
                //bug không gán giá trị cho CreateAt, khiến khi update nó tự gán giá trị default 0001-01-01 00:00:00.0000000. Mất dữ liệu gốc


                return await _roleRepository.Update(role);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "update operation failed");
                return false;
            }
        }
    }
}

using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Repositories.GenericRepository;

namespace CMC.TS.FT.Api.Repositories.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<bool> DeleteRolePermissionByRoleId(Guid roleId);
        Task<bool> AddRolePermissionByRoleId(RolePermission rolePermission);
        Task<bool> AddManyPermissionToRoleId(List<RolePermission> rolePermissions);
    }
}

using CMC.TS.FT.Api.Data;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Repositories.GenericRepository;
using CMC.TS.FT.Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CMC.TS.FT.Api.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly SQLServerDbContext _context;

        public RoleRepository(SQLServerDbContext context, ILogger<RoleRepository> logger) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteRolePermissionByRoleId(Guid roleId)
        {
            int affectRow = await _context.RolePermission.Where(rp => rp.RoleId == roleId).ExecuteDeleteAsync();
            return affectRow > 0;
        }

        public async Task<bool> AddRolePermissionByRoleId(RolePermission rolePermission)
        {
            _context.RolePermission.Add(rolePermission);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddManyPermissionToRoleId(List<RolePermission> rolePermissions)
        {
            _context.RolePermission.AddRange(rolePermissions);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}

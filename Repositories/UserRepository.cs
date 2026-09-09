using CMC.TS.FT.Api.Data;
using CMC.TS.FT.Api.DTO.Role;
using CMC.TS.FT.Api.DTO.User;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Repositories.GenericRepository;
using CMC.TS.FT.Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CMC.TS.FT.Api.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly SQLServerDbContext _context;
        public UserRepository(SQLServerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.User.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> UserSoftDelete(Guid id)
        {
            int rowAffect = await _context.User.Where(u => u.UserId == id).ExecuteUpdateAsync(setter => setter.SetProperty(user => user.IsDeleted, false));
            return rowAffect > 0;
        }

        public async Task<string?> GetPasswordById(Guid id) 
        {
            string? password = await _context.User.Where(u => u.UserId == id).Select(u=>u.PasswordHash).FirstOrDefaultAsync();
            return password;
        }

        public async Task<List<DisplayUserDTO>?> GetAllUserWithRoleName()
        {
            var a = from user in _context.User
                    join userRole in _context.UserRole on user.UserId equals userRole.UserId
                    join role in _context.Role on userRole.RoleId equals role.RoleId
                    where user.IsActive == true && user.IsDeleted == false
                    group role by user into g
                    select new DisplayUserDTO
                    {
                        UserId = g.Key.UserId,
                        Email = g.Key.Email,
                        Name = g.Key.Name,
                        IsActive = g.Key.IsActive,
                        IsDeleted = g.Key.IsDeleted,
                        roles = g.Select(r => new DisplayRoleDTO
                        {
                            RoleName = r.RoleName,
                        }).ToList()
                    };
            return await a.ToListAsync();
        }

        //for guest signing up
        public async Task AssignRoleGuest(Guid Userid)
        {
            Guid RoleId = await _context.Role.Where(r => r.RoleName == "customer").Select(r=>r.RoleId).FirstOrDefaultAsync();
            UserRole userRole = new UserRole
            {
                UserId = Userid,
                RoleId = RoleId,
                CreateAt = DateTime.UtcNow,
                CreateBy = Guid.Empty,
                UpdateAt = null,
                UpdateBy = Guid.Empty,
            };
            _context.UserRole.Add(userRole);
        }

        //for admin creating an account
        public async Task AssignRoleAdmin(Guid UserId, List<Guid> RoleIds)
        {
            List<UserRole> userRole = RoleIds.Select(r => new UserRole
            {
                UserId = UserId,
                RoleId = r,
                CreateAt = DateTime.UtcNow,
                CreateBy = Guid.Empty,
                UpdateAt = null,
                UpdateBy = Guid.Empty,
            }).ToList();
            _context.UserRole.AddRange(userRole);
            //return await _context.SaveChangesAsync() > 0;
        }
    }
}

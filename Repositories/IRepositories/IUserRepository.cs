using CMC.TS.FT.Api.DTO.Role;
using CMC.TS.FT.Api.DTO.User;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Repositories.GenericRepository;

namespace CMC.TS.FT.Api.Repositories.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
        Task<bool> UserSoftDelete(Guid id);
        Task<List<DisplayUserDTO>?> GetAllUserWithRoleName();
        Task AssignRoleGuest(Guid Userid);
        Task AssignRoleAdmin(Guid UserId, List<Guid> RoleIds);
        Task<string?> GetPasswordById(Guid id);
        Task<List<string>?> GetRoleNameByUserId(Guid id);
    }
}

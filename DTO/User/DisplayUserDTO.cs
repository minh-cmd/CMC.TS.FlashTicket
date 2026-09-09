using CMC.TS.FT.Api.DTO.Role;

namespace CMC.TS.FT.Api.DTO.User
{
    public class DisplayUserDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<DisplayRoleDTO>? roles { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

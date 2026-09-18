using System.ComponentModel.DataAnnotations;

namespace CMC.TS.FT.Api.DTO.Role
{
    public class AssignPermissionToRoleDTO
    {
        [Required]
        public Guid RoleId { get; set; }
        public List<Guid>? PermissionIds { get; set; }
    }
}

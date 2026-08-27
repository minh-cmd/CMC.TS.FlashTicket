namespace CMC.TS.FT.Api.DTO.Role
{
    public class AssignPermissionToRoleDTO
    {
        public Guid RoleId { get; set; }
        public List<Guid>? PermissionIds { get; set; }
    }
}

namespace CMC.TS.FT.Api.Entities
{
    public class RolePermission : AuditAbstractEntity
    {
        public Guid RoleId { get; set;}
        public Guid PermissionId { get; set;}
    }
}

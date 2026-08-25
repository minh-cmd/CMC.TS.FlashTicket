namespace CMC.TS.FT.Api.Entities
{
    public class Permission : AuditAbstractEntity
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }

        public Permission()
        {

        }
    }
}

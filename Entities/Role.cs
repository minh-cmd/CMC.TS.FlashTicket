namespace CMC.TS.FT.Api.Entities
{
    public class Role : AuditAbstractEntity
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        
        public Role()
        {

        }

        public Role(Guid id,  string roleName)
        {

        }
    }
}

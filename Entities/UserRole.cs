namespace CMC.TS.FT.Api.Entities
{
    public class UserRole : AuditAbstractEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public UserRole() 
        { 
        
        }
    }
}

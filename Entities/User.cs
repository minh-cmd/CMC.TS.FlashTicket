namespace CMC.TS.FT.Api.Entities
{
    public class User : AuditAbstractEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public string PasswordHash { get; set; }
        public User() { }
       
    }
}

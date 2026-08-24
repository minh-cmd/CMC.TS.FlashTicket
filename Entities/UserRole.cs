namespace CMC.TS.FT.Api.Entities
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid CreateBy { get; set; }
        public Guid UpdateBy { get; set; }
        public bool IsDeleted { get; set; }

        public UserRole() 
        { 
        
        }
    }
}

namespace CMC.TS.FT.Api.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public DateTime CreateAt {  get; set; }
        public DateTime? UpdateAt {  get; set; }
        public Guid CreateBy {  get; set; }
        public Guid UpdateBy {  get; set; }

        public bool IsDeleted { get; set; }
        
        public Role()
        {

        }

        public Role(Guid id,  string roleName)
        {

        }
    }
}

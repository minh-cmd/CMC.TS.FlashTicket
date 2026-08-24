namespace CMC.TS.FT.Api.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid CreateBy { get; set; }
        public Guid UpdateBy { get; set; }
        public bool IsDeleted { get; set; }
        public User() { }

        public User(Guid id, string name, string email, Guid roleId)
        {

        }
       
    }
}

namespace CMC.TS.FT.Api.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public Guid RoleId {  get; set; }
        public Guid CreatedAt {  get; set; }
        public bool IsDeleted {  get; set; }
        public User() { }

        public User(Guid id, string name, string email, Guid roleId)
        {

        }
       
    }
}

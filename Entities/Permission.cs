namespace CMC.TS.FT.Api.Entities
{
    public class Permission
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDisplayName { get; set; }
        public string PermissionDescription { get; set; }

        public Permission()
        {

        }
    }
}

namespace CMC.TS.FT.Api.DTO.User
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set;  }
        public List<Guid>? RoleIds { get; set; }
    }
}

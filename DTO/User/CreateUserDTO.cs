using System.ComponentModel.DataAnnotations;

namespace CMC.TS.FT.Api.DTO.User
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string PassWord { get; set;  }
        public List<Guid>? RoleIds { get; set; }
    }
}

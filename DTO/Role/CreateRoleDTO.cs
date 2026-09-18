using System.ComponentModel.DataAnnotations;

namespace CMC.TS.FT.Api.DTO.Role
{
    public class CreateRoleDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}

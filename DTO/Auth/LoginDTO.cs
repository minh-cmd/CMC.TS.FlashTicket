using System.ComponentModel.DataAnnotations;

namespace CMC.TS.FT.Api.DTO.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

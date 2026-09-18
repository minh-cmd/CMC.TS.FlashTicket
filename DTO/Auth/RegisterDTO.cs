using System.ComponentModel.DataAnnotations;

namespace CMC.TS.FT.Api.DTO.Auth
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }
    }
}

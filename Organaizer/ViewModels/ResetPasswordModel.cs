using System.ComponentModel.DataAnnotations;

namespace Organaizer.ViewModels
{
    public class ResetPasswordModel
    {
        [Required]
        [StringLength(maximumLength:40,MinimumLength =8),DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password)), DataType(DataType.Password)]
        public string CheckPass { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

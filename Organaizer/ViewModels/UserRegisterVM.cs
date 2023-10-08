using System.ComponentModel.DataAnnotations;

namespace Organaizer.ViewModels
{
    public class UserRegisterVM
    {
        

        
        [Required]
        [StringLength(maximumLength: 50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password)),DataType(DataType.Password)]
        public string CheckPass { get; set; }
        
        
        [StringLength(maximumLength: 200)]
        public string? ConfirmationToken { get; set; }
    }
}

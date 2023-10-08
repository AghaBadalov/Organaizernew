using System.ComponentModel.DataAnnotations;

namespace Organaizer.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        [StringLength(maximumLength:50),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength (maximumLength:40),DataType(DataType.Password)]
        
        public string Password { get; set; }
    }
}

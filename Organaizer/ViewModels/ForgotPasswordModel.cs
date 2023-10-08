using System.ComponentModel.DataAnnotations;

namespace Organaizer.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        [StringLength(maximumLength:50),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

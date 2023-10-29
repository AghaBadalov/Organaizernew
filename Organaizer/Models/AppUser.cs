using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Organaizer.Models
{
    public class AppUser :IdentityUser
    {
        [StringLength(maximumLength:200)]
        public string? ConfirmationToken { get; set; }
        public List<Basket> Baskets { get; set; }
    }
}

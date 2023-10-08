using System.ComponentModel.DataAnnotations;

namespace Organaizer.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(maximumLength:25)]
        public string? Key { get; set; }
        [StringLength(maximumLength: 50)]

        public string? Value { get; set; }
    }
}

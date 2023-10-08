using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organaizer.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        [StringLength(maximumLength: 75)]

        public string Desc { get; set; }
        [StringLength(maximumLength: 100)]

        
        public string Card { get; set; }
        [StringLength(maximumLength:105)]

        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

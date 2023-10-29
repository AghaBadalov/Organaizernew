using Organaizer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OrganaizerShop.Models
{
    public class OrganaizerModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(maximumLength:25)]
        public string Name { get; set; }
        [StringLength(maximumLength: 35)]

        public string Color { get; set; }
        public int Discount { get; set; }
        public double LPrice
        {
            get
            {
                if(Discount > 0)
                {
                    return Price - (Discount * Price) / 100;
                }

                return Price ;
            }
        }
        public double Price { get; set; }
        [StringLength(maximumLength: 45)]

        public string Type { get; set; }
        [StringLength(maximumLength: 45)]

        public string Purpose { get; set; }
        [StringLength(maximumLength: 110)]

        public string Desc { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<OrganaizerImages>? OrganaizerImages { get; set; }
        [NotMapped]
        public IFormFile? PosterImage { get;set; }
        [NotMapped]
        public List<IFormFile>? Images { get; set; }
        [NotMapped]
        public List<int>? OrgImagesIds { get; set; }
        public List<BasketItem>? BasketItems { get; set; }
        

    }
}

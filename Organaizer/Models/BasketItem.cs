using OrganaizerShop.Models;
using System.Drawing;

namespace Organaizer.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public double? Count { get; set; }
        public OrganaizerModel OrganaizerModel { get; set; }
        public int OrganaizerModelId { get; set; }
        
        public Basket Basket { get; set; }
        public int BasketId { get; set; }
        public bool IsOrdered { get; set; } = false;
    }
}

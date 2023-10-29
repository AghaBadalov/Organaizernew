namespace Organaizer.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}

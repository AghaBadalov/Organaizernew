namespace OrganaizerShop.Models
{
    public class OrganaizerImages
    {
        public int Id { get; set; }
        public int? OrganaizerModelId { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPoster { get; set; }
        public OrganaizerModel? OrganaizerModel { get; set; }
    }
}

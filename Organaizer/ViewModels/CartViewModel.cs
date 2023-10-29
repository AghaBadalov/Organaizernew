using Organaizer.Models;

namespace Organaizer.ViewModels
{
    public class CartViewModel
    {
        public List<BasketItemViewModel> BasketItems { get; set; } = new List<BasketItemViewModel>();
        public double Total { get; set; }
        public double LTotal { get; set; }

        public class BasketItemViewModel
        {
            public int OrganaizerModelId { get; set; }
            public string OrganaizerName { get; set; }
            public string Color { get; set; }

            public string ProductName { get; set; }
            public int Discount { get; set; }
            public double TotalPrice { get; set; }
            public double Price { get; set; }
            public double ProductPrice { get; set; }
            public double? ProductCount { get; set; }
            public string ImageUrl { get; set; }
            public double ProductTotal { get; set; }
        }
    }
}

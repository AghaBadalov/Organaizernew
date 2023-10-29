using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Organaizer.Models;
using OrganaizerShop.Models;

namespace Organaizer.DAL
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<OrganaizerModel> Organaizers { get; set; }
        public DbSet<OrganaizerImages> OrganaizerImages { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }


    }
}

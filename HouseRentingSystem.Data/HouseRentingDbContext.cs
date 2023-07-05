namespace HouseRentingSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using HouseRentingSystem.Data.Models;

    public class HouseRentingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public HouseRentingDbContext(DbContextOptions<HouseRentingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<House> Houses { get; set; } = null!;
        public DbSet<Agent> Agents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<House>()
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<House>()
                .HasOne(h => h.Agent)
                .WithMany(a => a.OwnedHouses)
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasData(SeedCategories());

            builder.Entity<House>()
                .HasData(SeedHouses());

            base.OnModelCreating(builder);
        }

        private Category[] SeedCategories()
        {
            HashSet<Category> categories = new HashSet<Category>();
            Category category;
            category = new Category()
            {
                Id = 1,
                Name = "Cottage"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Single-Family"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Duplex"
            };
            categories.Add(category);

            return categories.ToArray();
        }

        private House[] SeedHouses()
        {
            HashSet<House> houses = new HashSet<House>();
            House house;

            house = new House()
            {
                Title = "Big House Marina",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://www.luxury-architecture.net/wpcontent/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg",
                PricePerMonth = 2100.00M,
                CategoryId = 3,
                AgentId = Guid.Parse("FAE83FB9-1DDD-4C26-88EA-41815C572FF4"),
                RenterId = Guid.Parse("BA79E2A8-8AEB-492E-F613-08DB7C70D788")
            };
            houses.Add(house);

            house = new House()
            {
                Title = "Family House Comfort",
                Address = "Near the Sea Garden in Burgas, Bulgaria",
                Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                PricePerMonth = 1200.00M,
                CategoryId = 2,
                AgentId = Guid.Parse("FAE83FB9-1DDD-4C26-88EA-41815C572FF4"),
            };
            houses.Add(house);

            house = new House()
            {
                Title = "Grand House",
                Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                Description = "This luxurious house is everything you will need. It is just excellent.",
                ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
                PricePerMonth = 2000.00M,
                CategoryId = 2,
                AgentId = Guid.Parse("FAE83FB9-1DDD-4C26-88EA-41815C572FF4"),
            };
            houses.Add(house);

            return houses.ToArray();
        }
    }
}
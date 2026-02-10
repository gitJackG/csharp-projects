using FirstApi.Models;
using Microsoft.EntityFrameworkCore;
namespace FirstApi.Data
{
    public class FirstAPIContext : DbContext
    {
        public FirstAPIContext(DbContextOptions<FirstAPIContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shoe>().HasData(
                new Shoe
                {
                    Id = 1,
                    Name = "Nike SB",
                    Brand = "Nike",
                    Price = 100
                },
                new Shoe
                {
                    Id = 2,
                    Name = "Nike Air Force 1",
                    Brand = "Nike",
                    Price = 110
                }
                );
        }

        public DbSet<Shoe> Shoes {  get; set; }
    }
}

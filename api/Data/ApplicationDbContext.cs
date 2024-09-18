using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using api.Models.PhoneModels;
using api.Models.AuthenticationModels;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options) { }
        
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Color> Colors { get; set; }

        public DbSet<OtpStorage> OtpStorages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);



            modelBuilder.Entity<CartDetail>()
                .HasKey(rd => new { rd.CartID, rd.ProductID });
            modelBuilder.Entity<CartDetail>()
               .HasOne(rd => rd.Cart)
               .WithMany(r => r.CartDetails)
               .HasForeignKey(rd => rd.CartID);

          
            modelBuilder.Entity<Cart>()
                .HasOne(p => p.identityUser)  // Định nghĩa mối quan hệ
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<OtpStorage>(entity =>
            {
                entity.HasOne(o => o.identityUser)
                    .WithMany()
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(o => o.UserId);
            });

        }

     /*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=PhoneDB;User Id=root;Password=020403;",
                new MySqlServerVersion(new Version(8, 0, 33)));
        }*/

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
    }
}

using CarSellers.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarSellers.Data {
    public class DataContext : IdentityDbContext<AppUser> {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarSellerCompany> CarSellerCompanies { get; set; }
        public DbSet<AppUserCars> AppUserCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CarSellerCompany>().HasMany(csc => csc.Cars).WithOne(c => c.CarSellerCompany).HasForeignKey(c => c.CompanyID);
            modelBuilder.Entity<Manufacturer>().HasMany(m => m.CarModels).WithOne(cm => cm.Manufacturer).HasForeignKey(cm => cm.ManufacturerID);
            modelBuilder.Entity<CarModel>().HasMany(cm => cm.Cars).WithOne(c => c.CarModel).HasForeignKey(c => c.ModelID);
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole{
                    Name="User",
                    NormalizedName="USER"
                }
            };
            modelBuilder.Entity<AppUserCars>()
           .HasKey(uc => new { uc.AppUserId, uc.CarId });

            modelBuilder.Entity<AppUserCars>()
                .HasOne(uc => uc.AppUser)
                .WithMany(u => u.UserCars)
                .HasForeignKey(uc => uc.AppUserId);

            modelBuilder.Entity<AppUserCars>()
                .HasOne(uc => uc.Car)
                .WithMany(c => c.UserCars)
                .HasForeignKey(uc => uc.CarId);
            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }

    }
}

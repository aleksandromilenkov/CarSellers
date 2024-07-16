using CarSellers.Model;
using Microsoft.EntityFrameworkCore;

namespace CarSellers.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarSellerCompany> CarSellerCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<CarSellerCompany>().HasMany(csc => csc.Cars).WithOne(c => c.CarSellerCompany).HasForeignKey(c => c.CompanyID);
            modelBuilder.Entity<Manufacturer>().HasMany(m => m.CarModels).WithOne(cm => cm.Manufacturer).HasForeignKey(cm => cm.ManufacturerID);
            modelBuilder.Entity<CarModel>().HasMany(cm => cm.Cars).WithOne(c => c.CarModel).HasForeignKey(c => c.ModelID);
        }

    }
}

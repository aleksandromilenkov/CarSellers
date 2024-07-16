using CarSellers.Data;
using CarSellers.Model;

namespace CarSellers {
    public class Seed {
        private readonly DataContext dataContext;
        public Seed(DataContext context) {
            this.dataContext = context;
        }
        public void SeedDataContext() {
            // Check if database is created
            dataContext.Database.EnsureCreated();

            // Seed data if tables are empty
            if (!dataContext.CarSellerCompanies.Any()) {
                var company = new CarSellerCompany { CompanyName = "Fast Wheels", Address = "Ul. Pero Cico 32, Kumanovo" };
                var company2 = new CarSellerCompany { CompanyName = "Auto Welt", Address = "Ul. Nikola Karev 22, Kocani" };
                dataContext.CarSellerCompanies.Add(company);

                var toyota = new Manufacturer { ManufacturerName = "Toyota" };
                var ford = new Manufacturer { ManufacturerName = "Ford" };
                dataContext.Manufacturers.AddRange(toyota, ford);

                var corolla = new CarModel { ModelName = "Corolla", Manufacturer = toyota };
                var avensis = new CarModel { ModelName = "Avensis", Manufacturer = toyota };
                var mustang = new CarModel { ModelName = "Mustang", Manufacturer = ford };
                dataContext.CarModels.AddRange(corolla, avensis, mustang);

                dataContext.Cars.AddRange(
                    new Car { CarSellerCompany = company, CarModel = corolla, Year = 2020, Kilometers = 200000, Price = 12000 },
                    new Car { CarSellerCompany = company, CarModel = avensis, Year = 2021, Kilometers = 250000, Price = 10000 },
                    new Car { CarSellerCompany = company2, CarModel = mustang, Year = 2019, Kilometers = 300000, Price = 20000 }
                );

                dataContext.SaveChanges();

            }
            var manufacturerToUpdate = dataContext.Manufacturers.FirstOrDefault(m => m.ManufacturerName == "Ford");
            if (manufacturerToUpdate != null) {
                manufacturerToUpdate.Country = "USA";
                dataContext.SaveChanges();
            }

            var manufacturerToUpdate2 = dataContext.Manufacturers.FirstOrDefault(m => m.ManufacturerName == "Toyota");
            if (manufacturerToUpdate2 != null) {
                manufacturerToUpdate2.Country = "Japan";
                dataContext.SaveChanges();
            }
        }
    }
}

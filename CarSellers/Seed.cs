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
            //var company = new CarSellerCompany { CompanyName = "Fast Wheels", Address = "Ul. Pero Cico 32, Kumanovo" };
            //var company2 = new CarSellerCompany { CompanyName = "Auto Welt", Address = "Ul. Nikola Karev 22, Kocani" };
            //dataContext.CarSellerCompanies.Add(company);

            //var toyota = new Manufacturer { ManufacturerName = "Toyota", Country = "Japan" };
            //var ford = new Manufacturer { ManufacturerName = "Ford", Country = "USA" };
            var bmw = new Manufacturer { ManufacturerName = "BMW", Country = "Germany" };
            var audi = new Manufacturer { ManufacturerName = "Audi", Country = "Germany" };
            dataContext.Manufacturers.AddRange(bmw, audi);

            var bmw1 = new CarModel { ModelName = "520d", Manufacturer = bmw };
            var bmw2 = new CarModel { ModelName = "525d", Manufacturer = bmw };
            var bmw3 = new CarModel { ModelName = "530d", Manufacturer = bmw };
            var bmw144 = new CarModel { ModelName = "320d", Manufacturer = bmw };
            var bmw244 = new CarModel { ModelName = "330d", Manufacturer = bmw };
            var bmw344 = new CarModel { ModelName = "730d", Manufacturer = bmw };
            var bmw444 = new CarModel { ModelName = "740d", Manufacturer = bmw };
            var bmw5 = new CarModel { ModelName = "750d", Manufacturer = bmw };
            var bmw6 = new CarModel { ModelName = "X6", Manufacturer = bmw };
            var bmw7 = new CarModel { ModelName = "X5", Manufacturer = bmw };
            var bmw8 = new CarModel { ModelName = "X3", Manufacturer = bmw };
            var bmw9 = new CarModel { ModelName = "X1", Manufacturer = bmw };
            var bmw10 = new CarModel { ModelName = "Alpina", Manufacturer = bmw };
            var bmw11 = new CarModel { ModelName = "118i", Manufacturer = bmw };
            var bmw12 = new CarModel { ModelName = "120d", Manufacturer = bmw };
            var bmw13 = new CarModel { ModelName = "328i", Manufacturer = bmw };
            var bmw14 = new CarModel { ModelName = "318i", Manufacturer = bmw };
            var bmw15 = new CarModel { ModelName = "760i", Manufacturer = bmw };
            var bmw16 = new CarModel { ModelName = "740i", Manufacturer = bmw };
            var bmw17 = new CarModel { ModelName = "750i", Manufacturer = bmw };
            var bmw18 = new CarModel { ModelName = "330d xd", Manufacturer = bmw };
            var bmw19 = new CarModel { ModelName = "335d xd", Manufacturer = bmw };
            var bmw20 = new CarModel { ModelName = "330d xd", Manufacturer = bmw };
            var bmw21 = new CarModel { ModelName = "520i", Manufacturer = bmw };
            var bmw22 = new CarModel { ModelName = "528i", Manufacturer = bmw };
            var bmw23 = new CarModel { ModelName = "537i", Manufacturer = bmw };
            var bmw24 = new CarModel { ModelName = "640d", Manufacturer = bmw };
            dataContext.CarModels.AddRange(bmw1, bmw2, bmw3, bmw444, bmw344, bmw244, bmw144, bmw444, bmw5, bmw6, bmw7, bmw8, bmw9, bmw10, bmw11, bmw12, bmw13, bmw14, bmw15, bmw16, bmw17, bmw18, bmw19, bmw20, bmw21, bmw22, bmw23, bmw24);

            //dataContext.Cars.AddRange(
            //    new Car { CarSellerCompany = company, CarType = Enums.CarType.Sedan, CarColor = Enums.CarColor.Gray, CarModel = corolla, Year = 2020, Kilometers = 200000, Price = 12000 },
            //    new Car { CarSellerCompany = company, CarType = Enums.CarType.Hatchback, CarColor = Enums.CarColor.White, CarModel = avensis, Year = 2021, Kilometers = 250000, Price = 10000 },
            //    new Car { CarSellerCompany = company2, CarType = Enums.CarType.SportsCar, CarColor = Enums.CarColor.Black, CarModel = mustang, Year = 2019, Kilometers = 300000, Price = 20000 }
            //);

            dataContext.SaveChanges();



        }
    }
}

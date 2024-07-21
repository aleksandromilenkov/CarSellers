using CarSellers.Data;
using CarSellers.Helpers;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.EntityFrameworkCore;

namespace CarSellers.Repository {
    public class CarRepository : ICarRepository {
        private readonly DataContext _context;
        public CarRepository(DataContext context) {
            _context = context;
        }
        public async Task<bool> CarExists(int id) {
            return await _context.Cars.AnyAsync(c => c.CarID == id);
        }


        public async Task<bool> CreateCar(Car car) {
            await _context.Cars.AddAsync(car);
            return await Save();
        }

        public async Task<bool> DeleteCar(Car car) {
            _context.Cars.Remove(car);
            return await Save();
        }

        public async Task<ICollection<Car>> GetAllCars(CarQueryObject carQueryObject) {
            IQueryable<Car> cars = _context.Cars.Include(c => c.CarSellerCompany).Include(c => c.CarModel).AsQueryable();
            if (!string.IsNullOrWhiteSpace(carQueryObject.CompanyName)) {
                cars = cars.Where(c => c.CarSellerCompany.CompanyName.Trim().ToLower() == carQueryObject.CompanyName.Trim().ToLower());
            }
            if (!string.IsNullOrWhiteSpace(carQueryObject.ModelName)) {
                cars = cars.Where(c => c.CarModel.ModelName.Trim().ToLower() == carQueryObject.ModelName.Trim().ToLower());
            }
            if (carQueryObject.Year != null) {
                cars = cars.Where(c => c.Year == carQueryObject.Year);
            }
            if (!string.IsNullOrWhiteSpace(carQueryObject.KilometersFrom.ToString()) && !string.IsNullOrWhiteSpace(carQueryObject.KilometersTo.ToString())) {
                cars = cars.Where(c => c.Kilometers >= carQueryObject.KilometersFrom && c.Kilometers <= carQueryObject.KilometersTo);
            }
            if (!string.IsNullOrWhiteSpace(carQueryObject.PriceFrom.ToString()) && !string.IsNullOrWhiteSpace(carQueryObject.PriceTo.ToString())) {
                cars = cars.Where(c => c.Price >= carQueryObject.PriceFrom && c.Price <= carQueryObject.PriceTo);
            }
            if (!string.IsNullOrWhiteSpace(carQueryObject.SortBy)) {
                if (carQueryObject.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase)) {
                    cars = carQueryObject.IsDescending ? cars.OrderByDescending(c => c.Price) : cars.OrderBy(c => c.Price);
                }
                if (carQueryObject.SortBy.Equals("Kilometers", StringComparison.OrdinalIgnoreCase)) {
                    cars = carQueryObject.IsDescending ? cars.OrderByDescending(c => c.Kilometers) : cars.OrderBy(c => c.Kilometers);
                }
                if (carQueryObject.SortBy.Equals("Year", StringComparison.OrdinalIgnoreCase)) {
                    cars = carQueryObject.IsDescending ? cars.OrderByDescending(f => f.Year) : cars.OrderBy(f => f.Year);
                }
            }
            var skipNumber = (carQueryObject.PageNumber - 1) * carQueryObject.PageSize;
            return await cars.Skip(skipNumber).Take(carQueryObject.PageSize).ToListAsync();
        }

        public async Task<Car?> GetCarById(int id) {
            return await _context.Cars.Where(c => c.CarID == id).Include(c => c.CarSellerCompany).Include(c => c.CarModel).FirstOrDefaultAsync();
        }

        public async Task<bool> Save() {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCar(Car car) {
            _context.Cars.Update(car);
            return await Save();
        }
    }
}

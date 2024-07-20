using CarSellers.Data;
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

        public async Task<ICollection<Car>> GetAllCars() {
            return await _context.Cars.Include(c => c.CarSellerCompany).Include(c => c.CarModel).ThenInclude(c => c.Manufacturer).ToListAsync();
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

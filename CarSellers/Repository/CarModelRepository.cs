using CarSellers.Data;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.EntityFrameworkCore;

namespace CarSellers.Repository {
    public class CarModelRepository : ICarModelRepository {
        private readonly DataContext _context;
        public CarModelRepository(DataContext context) {
            _context = context;
        }
        public async Task<bool> CarModelExists(int id) {
            return await _context.CarModels.AnyAsync(cm => cm.ModelID == id);
        }

        public async Task<bool> CreateCarModel(CarModel carModel) {
            await _context.CarModels.AddAsync(carModel);
            return await Save();
        }

        public async Task<bool> DeleteCarModel(CarModel carModel) {
            _context.CarModels.Remove(carModel);
            return await Save();
        }

        public async Task<ICollection<CarModel>> GetAllCarModels() {
            return await _context.CarModels.Include(cm => cm.Manufacturer).ToListAsync();
        }

        public async Task<CarModel?> GetCarModelById(int id) {
            return await _context.CarModels.Where(cm => cm.ModelID == id).Include(cm => cm.Manufacturer).FirstOrDefaultAsync();
        }

        public async Task<bool> Save() {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCarModel(CarModel carModel) {
            _context.CarModels.Update(carModel);
            return await Save();
        }
    }
}

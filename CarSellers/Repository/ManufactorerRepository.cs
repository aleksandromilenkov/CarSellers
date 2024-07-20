using CarSellers.Data;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.EntityFrameworkCore;

namespace CarSellers.Repository {
    public class ManufactorerRepository : IManufacturerRepository {
        private readonly DataContext _context;
        public ManufactorerRepository(DataContext context) {
            _context = context;
        }

        public async Task<bool> CreateManufacturer(Manufacturer manufacturer) {
            await _context.Manufacturers.AddAsync(manufacturer);
            return await Save();
        }

        public async Task<bool> DeleteManufacturer(Manufacturer manufacturer) {
            _context.Manufacturers.Remove(manufacturer);
            return await Save();
        }

        public async Task<ICollection<Manufacturer>> GetAllManufacturers() {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer?> GetManufacturerById(int id) {
            return await _context.Manufacturers.Where(m => m.ManufacturerID == id).FirstOrDefaultAsync();
        }

        public async Task<bool> ManufacturerExists(int id) {
            return await _context.Manufacturers.AnyAsync(m => m.ManufacturerID == id);
        }

        public async Task<bool> Save() {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateManufacturer(Manufacturer manufacturer) {
            _context.Manufacturers.Update(manufacturer);
            return await Save();
        }
    }
}

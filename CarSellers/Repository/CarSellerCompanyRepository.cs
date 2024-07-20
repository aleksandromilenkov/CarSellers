using CarSellers.Data;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.EntityFrameworkCore;

namespace CarSellers.Repository {
    public class CarSellerCompanyRepository : ICarSellerCompanyRepository {
        private readonly DataContext _context;
        public CarSellerCompanyRepository(DataContext context) {
            _context = context;
        }

        public async Task<bool> CompanyExists(int id) {
            return await _context.CarSellerCompanies.AnyAsync(csc => csc.CompanyID == id);
        }

        public async Task<bool> CreateCompany(CarSellerCompany company) {
            await _context.CarSellerCompanies.AddAsync(company);
            return await Save();
        }

        public async Task<bool> DeleteCompany(CarSellerCompany company) {
            _context.CarSellerCompanies.Remove(company);
            return await Save();
        }

        public async Task<ICollection<CarSellerCompany>> GetAllCompanies() {
            return await _context.CarSellerCompanies.Include(c => c.Cars).ThenInclude(c => c.CarModel).ThenInclude(c => c.Manufacturer).ToListAsync();
        }

        public async Task<ICollection<Car>> GetCarsByCompanyId(int companyId) {
            return await _context.CarSellerCompanies.Where(c => c.CompanyID == companyId).SelectMany(c => c.Cars).ToListAsync();
        }

        public async Task<CarSellerCompany?> GetCompanyById(int id) {
            return await _context.CarSellerCompanies.Include(c => c.Cars).ThenInclude(c => c.CarModel).ThenInclude(c => c.Manufacturer).FirstOrDefaultAsync(csc => csc.CompanyID == id);
        }

        public async Task<bool> Save() {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCompany(CarSellerCompany company) {
            _context.CarSellerCompanies.Update(company);
            return await Save();
        }
    }
}

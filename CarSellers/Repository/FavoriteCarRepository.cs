using CarSellers.Data;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.EntityFrameworkCore;

namespace CarSellers.Repository {
    public class FavoriteCarRepository : IFavoriteCarsRepository {
        private readonly DataContext _context;

        public FavoriteCarRepository(DataContext context) {
            _context = context;
        }
        public async Task<AppUserCars> CreateUserFavoriteCar(AppUserCars userFavoriteCar) {
            await _context.AppUserCars.AddAsync(userFavoriteCar);
            await _context.SaveChangesAsync();
            return userFavoriteCar;
        }

        public async Task<AppUserCars> DeleteUserFavoriteCar(Car userFavoriteCar) {
           AppUserCars carToDelete = await _context.AppUserCars.Where(ac=> ac.CarId == userFavoriteCar.CarID).FirstOrDefaultAsync();
            _context.AppUserCars.Remove(carToDelete);
            await _context.SaveChangesAsync();
            return carToDelete;
        }

        public async Task<Car> GetUserFavoriteCarByCarId(int carId) {
            return await _context.Cars.Where(c => c.UserCars.Any(uc => uc.CarId == carId)).Include(c => c.CarModel).ThenInclude(cm => cm.Manufacturer).Include(c => c.CarSellerCompany).FirstOrDefaultAsync();
        }

        public async Task<Car> GetUserFavoriteCar(string userId, int carId) {
            return await _context.Cars.Where(c=>c.UserCars.Any(uc=>uc.AppUserId == userId && uc.CarId == carId)).Include(c=>c.CarModel).ThenInclude(cm => cm.Manufacturer).Include(c => c.CarSellerCompany).FirstOrDefaultAsync();

        }

        public async Task<List<Car>> GetUserFavoriteCars(AppUser user) {
            return await _context.Cars.Where(c => c.UserCars.Any(uc => uc.AppUserId == user.Id)).Include(c => c.CarModel).ThenInclude(cm=>cm.Manufacturer).Include(c=>c.CarSellerCompany).ToListAsync();

        }

        public async Task<Car> GetUserFavoriteCarByUserId(string userId) {
            return await _context.Cars.Where(c => c.UserCars.Any(uc => uc.AppUserId == userId)).Include(c => c.CarModel).ThenInclude(cm => cm.Manufacturer).FirstOrDefaultAsync();
        }
    }
}

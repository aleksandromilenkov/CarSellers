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

        public async Task<AppUserCars> DeleteUserFavoriteCar(AppUserCars userFavoriteCar) {
            _context.AppUserCars.Remove(userFavoriteCar);
            await _context.SaveChangesAsync();
            return userFavoriteCar;
        }

        public async Task<AppUserCars> GetUserFavoriteCarByCarId(int carId) {
            return await _context.AppUserCars.Where(p => p.CarId == carId).FirstOrDefaultAsync();
        }

        public async Task<AppUserCars> GetUserFavoriteCar(string userId, int carId) {
            return await _context.AppUserCars.Where(uc => uc.AppUserId == userId && uc.CarId == carId).FirstOrDefaultAsync();

        }

        public async Task<List<Car>> GetUserFavoriteCars(AppUser user) {
            return await _context.AppUserCars.Where(p => p.AppUserId == user.Id).Select(p => p.Car).ToListAsync();
        }

        public async Task<AppUserCars> GetUserFavoriteCarByUserId(string userId) {
            return await _context.AppUserCars.Where(p => p.AppUserId == userId).FirstOrDefaultAsync();
        }
    }
}

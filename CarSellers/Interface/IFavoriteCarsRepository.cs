using CarSellers.Model;

namespace CarSellers.Interface {
    public interface IFavoriteCarsRepository {
        Task<List<Car>> GetUserFavoriteCars(AppUser user);
        Task<AppUserCars> CreateUserFavoriteCar(AppUserCars userFavoriteCar);
        Task<AppUserCars> DeleteUserFavoriteCar(AppUserCars userFavoriteCar);
        Task<AppUserCars> GetUserFavoriteCarByUserId(string userId);
        Task<AppUserCars> GetUserFavoriteCarByCarId(int carId);
        Task<AppUserCars> GetUserFavoriteCar(string userId, int carId);
    }
}

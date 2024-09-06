using CarSellers.Model;

namespace CarSellers.Interface {
    public interface IFavoriteCarsRepository {
        Task<List<Car>> GetUserFavoriteCars(AppUser user);
        Task<AppUserCars> CreateUserFavoriteCar(AppUserCars userFavoriteCar);
        Task<AppUserCars> DeleteUserFavoriteCar(Car userFavoriteCar);
        Task<Car> GetUserFavoriteCarByUserId(string userId);
        Task<Car> GetUserFavoriteCarByCarId(int carId);
        Task<Car> GetUserFavoriteCar(string userId, int carId);
    }
}

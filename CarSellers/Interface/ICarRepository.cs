using CarSellers.Helpers;
using CarSellers.Model;

namespace CarSellers.Interface {
    public interface ICarRepository {
        Task<ICollection<Car>> GetAllCars(CarQueryObject carQueryObject);
        Task<Car?> GetCarById(int id);
        Task<bool> CreateCar(Car car);
        Task<bool> UpdateCar(Car car);
        Task<bool> DeleteCar(Car car);
        Task<bool> CarExists(int id);
        Task<bool> Save();
    }
}

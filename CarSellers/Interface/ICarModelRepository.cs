using CarSellers.Model;

namespace CarSellers.Interface {
    public interface ICarModelRepository {
        Task<ICollection<CarModel>> GetAllCarModels();
        Task<CarModel?> GetCarModelById(int id);
        Task<bool> CreateCarModel(CarModel carModel);
        Task<bool> UpdateCarModel(CarModel carModel);
        Task<bool> DeleteCarModel(CarModel carModel);
        Task<bool> CarModelExists(int id);
        Task<bool> Save();
    }
}

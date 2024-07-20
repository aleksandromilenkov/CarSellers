using CarSellers.Model;

namespace CarSellers.Interface {

    public interface IManufacturerRepository {
        Task<ICollection<Manufacturer>> GetAllManufacturers();
        Task<Manufacturer?> GetManufacturerById(int id);
        Task<bool> CreateManufacturer(Manufacturer manufacturer);
        Task<bool> UpdateManufacturer(Manufacturer manufacturer);
        Task<bool> DeleteManufacturer(Manufacturer manufacturer);
        Task<bool> ManufacturerExists(int id);
        Task<bool> Save();
    }
}

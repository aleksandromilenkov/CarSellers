using CarSellers.Model;

namespace CarSellers.Interface {
    public interface ICarSellerCompanyRepository {
        Task<ICollection<CarSellerCompany>> GetAllCompanies();
        Task<CarSellerCompany?> GetCompanyById(int id);
        Task<CarSellerCompany?> GetCompanyByIdAsNoTracking(int id);
        Task<ICollection<Car>> GetCarsByCompanyId(int companyId);
        Task<bool> CreateCompany(CarSellerCompany company);
        Task<bool> UpdateCompany(CarSellerCompany company);
        Task<bool> DeleteCompany(CarSellerCompany company);
        Task<bool> CompanyExists(int id);
        Task<bool> Save();
    }
}

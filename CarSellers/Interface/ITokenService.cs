using CarSellers.Model;

namespace CarSellers.Interface {
    public interface ITokenService {
        Task<string> CreateToken(AppUser appUser);
    }
}

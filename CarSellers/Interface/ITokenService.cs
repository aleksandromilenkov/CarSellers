using CarSellers.Model;

namespace CarSellers.Interface {
    public interface ITokenService {
        string CreateToken(AppUser appUser);
    }
}

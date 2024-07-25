using Microsoft.AspNetCore.Identity;

namespace CarSellers.Model {
    public class AppUser : IdentityUser {
        public ICollection<AppUserCars> UserCars { get; set; }
    }
}

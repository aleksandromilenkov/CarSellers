using Microsoft.AspNetCore.Identity;

namespace CarSellers.Model {
    public class AppUser : IdentityUser {
        public string? ProfilePicture { get; set; } // Path to the profile picture
        public ICollection<AppUserCars> UserCars { get; set; }
    }
}

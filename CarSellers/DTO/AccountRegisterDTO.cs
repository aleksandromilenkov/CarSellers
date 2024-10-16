using System.ComponentModel.DataAnnotations;

namespace CarSellers.DTO {
    public class AccountRegisterDTO {
        [Required]
        [MinLength(2, ErrorMessage = "Username must be at least 2 chracters")]
        [MaxLength(15, ErrorMessage = "Username must be maximum 15 characters")]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Password must be at least 5 chracters")]
        [MaxLength(35, ErrorMessage = "Password must be maximum 35 characters")]
        public string? Password { get; set; }
        // Add the profile picture
        public IFormFile? ProfileImage { get; set; }
    }
}

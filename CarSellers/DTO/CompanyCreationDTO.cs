using System.ComponentModel.DataAnnotations;

namespace CarSellers.DTO {
    public class CompanyCreationDTO {
        [Required]
        [MinLength(2, ErrorMessage = "CompanyName must be at least 2 chracters")]
        [MaxLength(65, ErrorMessage = "CompanyName must be maximum 65 characters")]
        public string CompanyName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Address must be at least 2 chracters")]
        [MaxLength(65, ErrorMessage = "Address must be maximum 65 characters")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string? TelephoneNumber { get; set; }
        public IFormFile? CompanyImage { get; set; }
    }
}

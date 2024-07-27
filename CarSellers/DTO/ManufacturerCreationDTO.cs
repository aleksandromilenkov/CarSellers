using System.ComponentModel.DataAnnotations;

namespace CarSellers.DTO {
    public class ManufacturerCreationDTO {
        [Required]
        [MinLength(2, ErrorMessage = "ManufacturerName must be at least 2 chracters")]
        [MaxLength(65, ErrorMessage = "ManufacturerName must be maximum 65 characters")]
        public string ManufacturerName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Country must be at least 2 chracters")]
        [MaxLength(65, ErrorMessage = "Country must be maximum 65 characters")]
        public string Country { get; set; }
    }
}

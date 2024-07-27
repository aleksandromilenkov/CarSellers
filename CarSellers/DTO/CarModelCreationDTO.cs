using System.ComponentModel.DataAnnotations;

namespace CarSellers.DTO {
    public class CarModelCreationDTO {
        [Required]
        [MinLength(2, ErrorMessage = "ModelName must be at least 2 chracters")]
        [MaxLength(15, ErrorMessage = "ModelName must be maximum 15 characters")]
        public string ModelName { get; set; }
        [Required]
        public int ManufacturerID { get; set; }
    }
}

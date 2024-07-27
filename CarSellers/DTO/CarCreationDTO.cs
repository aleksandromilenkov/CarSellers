using CarSellers.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSellers.DTO {
    public class CarCreationDTO {
        [Required(ErrorMessage = "The Year is required")]
        public int Year { get; set; }
        [Required(ErrorMessage = "The Km is required")]
        public int Kilometers { get; set; }
        [Required(ErrorMessage = "The Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The Model is required")]
        public int ModelID { get; set; }
        public CarType CarType { get; set; }
        public CarColor CarColor { get; set; }
        public CarOwner CarOwner { get; set; }
        public CarRegistration CarRegistration { get; set; }
        [Required(ErrorMessage = "The Company is required")]
        public int CompanyID { get; set; }
    }
}

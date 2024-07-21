using CarSellers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSellers.Model {
    public class Car {
        [Key]
        public int CarID { get; set; }
        [ForeignKey("CarModel")]
        public int ModelID { get; set; }
        [ForeignKey("CarSellerCompany")]
        public int CompanyID { get; set; }
        public int Year { get; set; }
        public int Kilometers { get; set; }
        public CarType CarType { get; set; } = CarType.Sedan;
        public CarColor CarColor { get; set; } = CarColor.White;
        public decimal Price { get; set; }
        public CarModel? CarModel { get; set; }
        public CarSellerCompany? CarSellerCompany { get; set; }
    }
}

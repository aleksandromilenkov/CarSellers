using CarSellers.Enums;

namespace CarSellers.DTO {
    public class CarCreationDTO {
        public int Year { get; set; }
        public int Kilometers { get; set; }
        public decimal Price { get; set; }
        public int ModelID { get; set; }
        public CarType CarType { get; set; }
        public CarColor CarColor { get; set; }
        public CarOwner CarOwner { get; set; }
        public CarRegistration CarRegistration { get; set; }
        public int CompanyID { get; set; }
    }
}

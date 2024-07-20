using CarSellers.Model;

namespace CarSellers.DTO {
    public class CarModelDTO {
        public int ModelID { get; set; }
        public string? ModelName { get; set; }
        public int ManufacturerID { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}

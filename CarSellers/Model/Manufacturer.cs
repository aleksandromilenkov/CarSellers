using System.ComponentModel.DataAnnotations;

namespace CarSellers.Model {
    public class Manufacturer {
        [Key]
        public int ManufacturerID { get; set; }
        public string? ManufacturerName { get; set; }
        public string? Country { get; set; }
        public IEnumerable<CarModel> CarModels { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSellers.Model {
    public class CarModel {
        [Key]
        public int ModelID { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ModelName { get; set; }
        [ForeignKey("Manufacturer")]
        public int ManufacturerID { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}

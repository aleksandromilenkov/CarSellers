using System.ComponentModel.DataAnnotations;

namespace CarSellers.Model {
    public class CarSellerCompany {
        [Key]
        public int CompanyID { get; set; }
        [Required]
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public IEnumerable<Car> Cars { get; set; }


    }
}

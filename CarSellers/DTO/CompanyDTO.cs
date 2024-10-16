using CarSellers.Model;

namespace CarSellers.DTO {
    public class CompanyDTO {
        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? CompanyImage { get; set; }
        public IEnumerable<Car> Cars { get; set; }

    }
}

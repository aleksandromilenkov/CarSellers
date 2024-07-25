using CarSellers.Enums;

namespace CarSellers.Helpers {
    public class CarQueryObject {
        public string? CompanyName { get; set; }
        public string? ModelName { get; set; }
        public int? Year { get; set; }
        public int? KilometersFrom { get; set; }
        public int? KilometersTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public CarType? CarType { get; set; }
        public CarColor? CarColor { get; set; }
        public CarRegistration? CarRegistration { get; set; }
        public CarOwner? CarOwner { get; set; }
    }
}

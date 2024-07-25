namespace CarSellers.Model {
    public class AppUserCars {
        public string AppUserId { get; set; }
        public int CarId { get; set; }
        public AppUser AppUser { get; set; }
        public Car Car { get; set; }
    }
}

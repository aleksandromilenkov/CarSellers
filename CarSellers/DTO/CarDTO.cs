﻿using CarSellers.Model;

namespace CarSellers.DTO {
    public class CarDTO {
        public int CarID { get; set; }
        public int Year { get; set; }
        public int Kilometers { get; set; }
        public decimal Price { get; set; }
        public CarModel CarModel { get; set; }
        public CarSellerCompany CarSellerCompany { get; set; }
    }
}

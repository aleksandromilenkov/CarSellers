﻿using CarSellers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

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
        public string? CarImage { get; set; }
        public CarType? CarType { get; set; }
        public CarColor? CarColor { get; set; }
        public CarRegistration? CarRegistration { get; set; }
        public CarOwner? CarOwner { get; set; }
        public decimal Price { get; set; }
        public CarModel? CarModel { get; set; }
        public CarSellerCompany? CarSellerCompany { get; set; }
        public ICollection<AppUserCars> UserCars { get; set; }
    }
}

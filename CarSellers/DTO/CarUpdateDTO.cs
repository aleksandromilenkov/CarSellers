﻿using CarSellers.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSellers.DTO
{
    public class CarUpdateDTO
    {
        [Required(ErrorMessage = "The Year is required")]
        public int Year { get; set; }
        [Required(ErrorMessage = "The Km is required")]
        public int Kilometers { get; set; }
        [Required(ErrorMessage = "The Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The Model is required")]
        public int ModelID { get; set; }
        [NotMapped]
        public IFormFile? CarImage { get; set; }
        public CarType? CarType { get; set; }
        public CarColor? CarColor { get; set; }
        public CarOwner? CarOwner { get; set; }
        public CarRegistration? CarRegistration { get; set; }
        [Required(ErrorMessage = "The Company is required")]
        public int CompanyID { get; set; }
    }
}

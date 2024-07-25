﻿using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteCarsController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFavoriteCarsRepository _favoriteCarsRepository;
        private readonly ICarRepository _carRepository;

        public FavoriteCarsController(UserManager<AppUser> userManager, IFavoriteCarsRepository favoriteCarsRepository, ICarRepository carRepository) {
            this._favoriteCarsRepository = favoriteCarsRepository;
            _userManager = userManager;
            _carRepository = carRepository;
        }

        [HttpGet(Name = "GetUserFavoriteCar")]
        [Authorize]
        public async Task<IActionResult> GetAppUserFavoriteCars() {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username)) {
                return Unauthorized("User not found.");
            }
            var appUser = await _userManager.FindByNameAsync(username);
            var userFavoriteCars = await _favoriteCarsRepository.GetUserFavoriteCars(appUser);
            return Ok(userFavoriteCars);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CrteateUserFavoriteCar(int carId) {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username)) {
                return Unauthorized("User not found.");
            }
            var appUser = await _userManager.FindByNameAsync(username);
            var car = await _carRepository.GetCarById(carId);

            var userFavoriteCars = await _favoriteCarsRepository.GetUserFavoriteCars(appUser);
            if (userFavoriteCars.Any(c => c.CarID == carId)) {
                return BadRequest("You already have this car in your favorites.");
            }
            var userFavoriteCarsToCreate = new AppUserCars {
                CarId = carId,
                AppUserId = appUser.Id,
            };
            var userFavoriteCarsToReturn = await _favoriteCarsRepository.CreateUserFavoriteCar(userFavoriteCarsToCreate);
            if (userFavoriteCarsToReturn == null) {
                return StatusCode(500, "Could not create user favorite car.");
            }
            else {
                return CreatedAtRoute("GetUserFavoriteCar", null);
            }
        }

        [HttpDelete("{symbol}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserPortfolio([FromRoute] int carId) {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username)) {
                return Unauthorized("User not found.");
            }
            var appUser = await _userManager.FindByNameAsync(username);
            var car = await _favoriteCarsRepository.GetUserFavoriteCar(appUser.Id, carId);
            if (car == null) {
                return BadRequest("Car is not in your Favorites.");
            }

            var deletedFavoriteCar = await _favoriteCarsRepository.DeleteUserFavoriteCar(car);
            return NoContent();
        }


    }
}
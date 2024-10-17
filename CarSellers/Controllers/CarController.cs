using AutoMapper;
using CarSellers.DTO;
using CarSellers.Helpers;
using CarSellers.Interface;
using CarSellers.Model;
using CarSellers.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CarController(ICarRepository carRepository, IMapper mapper, IFileService fileService) {
            this._carRepository = carRepository;
            this._mapper = mapper;
            this._fileService = fileService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public async Task<IActionResult> GetCars([FromQuery] CarQueryObject carQueryObject) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var cars = await _carRepository.GetAllCars(carQueryObject);

            IEnumerable<CarDTO> carsToReturn = _mapper.Map<IEnumerable<CarDTO>>(cars);
            return Ok(carsToReturn);
        }

            
        [HttpGet("{carId:int}", Name = "GetCar")]
        [ProducesResponseType(200, Type = typeof(CarDTO))]
        public async Task<IActionResult> GetCarById([FromRoute] int carId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var car = await _carRepository.GetCarById(carId);
            if (car == null) {
                ModelState.AddModelError("", "Car Not found");
                return NotFound();
            }

            CarDTO carToReturn = _mapper.Map<CarDTO>(car);
            return Ok(carToReturn);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CarDTO))]
        public async Task<IActionResult> CreateCar([FromForm] CarCreationDTO car) {
            try
            {
                if (car == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (car.CarImage?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = {".jpg", ".jpeg", ".png"};
                string createdImageName = await _fileService.SaveFileAsync(car.CarImage, allowedFileExtentions);
              
                Car mappedToCar = _mapper.Map<Car>(car);
                mappedToCar.CarImage = createdImageName;
                if (!await _carRepository.CreateCar(mappedToCar))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                CarDTO carToReturn = _mapper.Map<CarDTO>(mappedToCar);
                return CreatedAtRoute("GetCar", new { carId = carToReturn.CarID }, carToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{carId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCar([FromRoute] int carId, [FromForm] CarUpdateDTO car) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                if (car == null) {
                    return BadRequest(ModelState);
                }

                if (!await _carRepository.CarExists(carId)) {
                    return NotFound();
                }

                var existingCar = await _carRepository.GetCarByIdAsNoTracking(carId);
                if (existingCar == null) {
                        return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {carId} does not found");
                }

                string? oldImage = existingCar?.CarImage;
                var carMap = _mapper.Map<Car>(car);
                carMap.CarID = carId;

                if (car.CarImage != null) {
                    if (car.CarImage?.Length > 1 * 1024 * 1024) {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                    }
                    string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                    string createdImageName = await _fileService.SaveFileAsync(car.CarImage, allowedFileExtentions);
                    carMap.CarImage = createdImageName;
                    if(oldImage != null) _fileService.DeleteFile(oldImage);
                } else
                {
                    carMap.CarImage = oldImage;
                }

                if (!await _carRepository.UpdateCar(carMap)) {
                    return BadRequest(ModelState);
                }
                    return NoContent();
                }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{carId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!await _carRepository.CarExists(carId))
                {
                    return NotFound();
                }


                Car? car = await _carRepository.GetCarById(carId);
                if (car == null)
                {
                    return NotFound();
                }
                if (!await _carRepository.DeleteCar(car))
                {
                    ModelState.AddModelError("", "Something went wrong with the deleting");
                    return StatusCode(500, ModelState);
                }
                // After deleting car from database,remove file from directory.
                if (car?.CarImage != null)
                {
                    _fileService.DeleteFile(car.CarImage);
                }
                return Ok("Successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        }
}

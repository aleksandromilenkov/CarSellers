using AutoMapper;
using CarSellers.DTO;
using CarSellers.Helpers;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase {
        private readonly ICarRepository _carRepository;

        private readonly IMapper _mapper;
        public CarController(ICarRepository carRepository, IMapper mapper) {
            this._carRepository = carRepository;

            this._mapper = mapper;
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
        public async Task<IActionResult> CreateCar([FromBody] CarCreationDTO car) {
            if (car == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Car mappedToCar = _mapper.Map<Car>(car);
            if (!await _carRepository.CreateCar(mappedToCar)) {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            CarDTO carToReturn = _mapper.Map<CarDTO>(mappedToCar);
            return CreatedAtRoute("GetCar", new { carId = carToReturn.CarID }, carToReturn);
        }


        [HttpPut("{carId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCar([FromRoute] int carId, [FromBody] CarCreationDTO car) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (car == null) {
                return BadRequest(ModelState);
            }
            if (!await _carRepository.CarExists(carId)) {
                return NotFound();
            }
            var carMap = _mapper.Map<Car>(car);
            carMap.CarID = carId;
            if (!await _carRepository.UpdateCar(carMap)) {
                return BadRequest(ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{carId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCar(int carId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (!await _carRepository.CarExists(carId)) {
                return NotFound();
            }


            Car? car = await _carRepository.GetCarById(carId);
            if (car == null) {
                return NotFound();
            }
            if (!await _carRepository.DeleteCar(car)) {
                ModelState.AddModelError("", "Something went wrong with the deleting");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}

using AutoMapper;
using CarSellers.DTO;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase {
        private readonly ICarModelRepository _carModelRepository;

        private readonly IMapper _mapper;
        public CarModelController(ICarModelRepository carModelRepository, IMapper mapper) {
            this._carModelRepository = carModelRepository;

            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarModelDTO>))]
        public async Task<IActionResult> GetCarModels() {
            var carModels = await _carModelRepository.GetAllCarModels();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            IEnumerable<CarModelDTO> carModelsToReturn = _mapper.Map<IEnumerable<CarModelDTO>>(carModels);
            return Ok(carModelsToReturn);
        }


        [HttpGet("{carModelId}", Name = "GetCarModel")]
        [ProducesResponseType(200, Type = typeof(CarModelDTO))]
        public async Task<IActionResult> GetCarModelById([FromRoute] int carModelId) {
            var carModel = await _carModelRepository.GetCarModelById(carModelId);
            if (carModel == null) {
                ModelState.AddModelError("", "CarModel Not found");
                return NotFound();
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            CarModelDTO carModelToReturn = _mapper.Map<CarModelDTO>(carModel);
            return Ok(carModelToReturn);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CarModelDTO))]
        public async Task<IActionResult> CreateCarModel([FromBody] CarModelCreationDTO carModel) {
            if (carModel == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            CarModel mappedToCarModel = _mapper.Map<CarModel>(carModel);
            if (!await _carModelRepository.CreateCarModel(mappedToCarModel)) {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            CarModelDTO carModelToReturn = _mapper.Map<CarModelDTO>(mappedToCarModel);
            return CreatedAtRoute("GetCarModel", new { carModelId = carModelToReturn.ModelID }, carModelToReturn);
        }


        [HttpPut("{carModelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCarModel([FromRoute] int carModelId, [FromBody] CarModelCreationDTO carModel) {
            if (carModel == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (!await _carModelRepository.CarModelExists(carModelId)) {
                return NotFound();
            }
            var carModelMap = _mapper.Map<CarModel>(carModel);
            carModelMap.ModelID = carModelId;
            if (!await _carModelRepository.UpdateCarModel(carModelMap)) {
                return BadRequest(ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{carModelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCarModel(int carModelId) {
            if (!await _carModelRepository.CarModelExists(carModelId)) {
                return NotFound();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            CarModel? carModel = await _carModelRepository.GetCarModelById(carModelId);
            if (carModel == null) {
                return NotFound();
            }
            if (!await _carModelRepository.DeleteCarModel(carModel)) {
                ModelState.AddModelError("", "Something went wrong with the deleting");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}

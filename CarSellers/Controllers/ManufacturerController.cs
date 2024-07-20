using AutoMapper;
using CarSellers.DTO;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase {
        private readonly IManufacturerRepository _manufactorerRepository;

        private readonly IMapper _mapper;
        public ManufacturerController(IManufacturerRepository manufactorerRepository, IMapper mapper) {
            this._manufactorerRepository = manufactorerRepository;

            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ManufacturerDTO>))]
        public async Task<IActionResult> GetManufacturers() {
            var manufacturers = await _manufactorerRepository.GetAllManufacturers();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            IEnumerable<ManufacturerDTO> manufacturersToReturn = _mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers);
            return Ok(manufacturersToReturn);
        }


        [HttpGet("{manufacturerId}", Name = "GetManufacturer")]
        [ProducesResponseType(200, Type = typeof(ManufacturerDTO))]
        public async Task<IActionResult> GetManufacturerById([FromRoute] int manufacturerId) {
            var manufacturer = await _manufactorerRepository.GetManufacturerById(manufacturerId);
            if (manufacturer == null) {
                ModelState.AddModelError("", "Manufacturer Not found");
                return NotFound();
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            ManufacturerDTO manufacturerToReturn = _mapper.Map<ManufacturerDTO>(manufacturer);
            return Ok(manufacturerToReturn);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ManufacturerDTO))]
        public async Task<IActionResult> CreateManufacturer([FromBody] ManufacturerCreationDTO manufacturer) {
            if (manufacturer == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Manufacturer mappedToManufacturer = _mapper.Map<Manufacturer>(manufacturer);
            if (!await _manufactorerRepository.CreateManufacturer(mappedToManufacturer)) {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            ManufacturerDTO manufacturerToReturn = _mapper.Map<ManufacturerDTO>(mappedToManufacturer);
            return CreatedAtRoute("GetManufacturer", new { manufacturerId = manufacturerToReturn.ManufacturerID }, manufacturerToReturn);
        }


        [HttpPut("{manufacturerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateManufacturer([FromRoute] int manufacturerId, [FromBody] ManufacturerCreationDTO manufacturer) {
            if (manufacturer == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (!await _manufactorerRepository.ManufacturerExists(manufacturerId)) {
                return NotFound();
            }
            var manufacturerMap = _mapper.Map<Manufacturer>(manufacturer);
            manufacturerMap.ManufacturerID = manufacturerId;
            if (!await _manufactorerRepository.UpdateManufacturer(manufacturerMap)) {
                return BadRequest(ModelState);
            }
            return NoContent();

        }


        [HttpDelete("{manufacturerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteManufacturer(int manufacturerId) {
            if (!await _manufactorerRepository.ManufacturerExists(manufacturerId)) {
                return NotFound();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Manufacturer? manufacturer = await _manufactorerRepository.GetManufacturerById(manufacturerId);
            if (manufacturer == null) {
                return NotFound();
            }
            if (!await _manufactorerRepository.DeleteManufacturer(manufacturer)) {
                ModelState.AddModelError("", "Something went wrong with the deleting");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}

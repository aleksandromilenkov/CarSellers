﻿using AutoMapper;
using CarSellers.DTO;
using CarSellers.Interface;
using CarSellers.Model;
using CarSellers.Service;
using Microsoft.AspNetCore.Mvc;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarSellerCompanyController : ControllerBase {
        private readonly ICarSellerCompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CarSellerCompanyController(ICarSellerCompanyRepository carSellerCompany, IMapper mapper, IFileService fileService) {
            this._companyRepository = carSellerCompany;
            this._mapper = mapper;
            this._fileService = fileService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CompanyDTO>))]
        public async Task<IActionResult> GetCompanies() {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var carSellerCompanies = await _companyRepository.GetAllCompanies();
            var companiesToDisplay = _mapper.Map<List<CompanyDTO>>(carSellerCompanies);

            return Ok(companiesToDisplay);
        }

        [HttpGet("{companyId:int}", Name = "GetCompany")]
        [ProducesResponseType(200, Type = typeof(CompanyDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCompanyById(int companyId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (!await _companyRepository.CompanyExists(companyId)) {
                return NotFound();
            }

            var company = await _companyRepository.GetCompanyById(companyId);
            var companyToDisplay = _mapper.Map<CompanyDTO>(company);
            return Ok(companyToDisplay);
        }

        [HttpGet("cars/{companyId:int}", Name = "GetCarsByCompanyId")]
        [ProducesResponseType(200, Type = typeof(List<CarDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCarsByCompanyId([FromRoute] int companyId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (!await _companyRepository.CompanyExists(companyId)) {
                return NotFound();
            }

            var cars = await _companyRepository.GetCarsByCompanyId(companyId);
            var carsToDisplay = _mapper.Map<List<CarDTO>>(cars);
            return Ok(carsToDisplay);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CompanyDTO))]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CompanyDTO>> CreateCompany([FromForm] CompanyCreationDTO companyCreate) {
            if (companyCreate == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var companies = await _companyRepository.GetAllCompanies();
            var companyExists = companies.Where(c => c.CompanyName.Trim().ToLower() == companyCreate.CompanyName.Trim().ToLower()).FirstOrDefault();
            if (companyExists != null) {
                ModelState.AddModelError("", "Company already exists");
                return StatusCode(422, ModelState);
            }
            var companyMap = _mapper.Map<CarSellerCompany>(companyCreate);
            if (companyCreate.CompanyImage != null)
            {
                if (companyCreate.CompanyImage?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                string createdImageName = await _fileService.SaveFileAsync(companyCreate.CompanyImage, allowedFileExtentions);
                companyMap.CompanyImage = createdImageName;
            }
            if (!await _companyRepository.CreateCompany(companyMap)) {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var companyToReturn = _mapper.Map<CompanyDTO>(companyMap);
            return CreatedAtRoute("GetCompany", new { companyId = companyToReturn.CompanyID }, companyToReturn);
        }

        [HttpPut("{companyId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCompany([FromRoute] int companyId, [FromForm] CompanyCreationDTO company) {
            if (company == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var existingCompany =await _companyRepository.GetCompanyByIdAsNoTracking(companyId);
            if (existingCompany == null) {
                return NotFound();
            }
            string? oldImage = existingCompany?.CompanyImage;
            var companyMap = _mapper.Map<CarSellerCompany>(company);
            if (company.CompanyImage != null)
            {
                if (company.CompanyImage?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                string createdImageName = await _fileService.SaveFileAsync(company.CompanyImage, allowedFileExtentions);
                companyMap.CompanyImage = createdImageName;
                if(oldImage != null) _fileService.DeleteFile(oldImage);
            } else
            {
                companyMap.CompanyImage = oldImage;
            }
            companyMap.CompanyID = companyId;
            if (!await _companyRepository.UpdateCompany(companyMap)) {
                return BadRequest(ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{companyId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCompany(int companyId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (!await _companyRepository.CompanyExists(companyId)) {
                return NotFound();
            }


            CarSellerCompany company = await _companyRepository.GetCompanyById(companyId);
            if (!await _companyRepository.DeleteCompany(company)) {
                ModelState.AddModelError("", "Something went wrong with the deleting");
                return StatusCode(500, ModelState);
            }
            if (company?.CompanyImage != null)
            {
                _fileService.DeleteFile(company.CompanyImage);
            }
            return Ok("Successfully deleted");
        }
    }
}

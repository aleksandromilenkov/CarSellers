using AutoMapper;
using CarSellers.DTO;
using CarSellers.Model;

namespace CarSellers.AutoMapperProfiles {
    public class MappingProfiles : Profile {
        public MappingProfiles() {
            CreateMap<CompanyDTO, CarSellerCompany>().ReverseMap();
            CreateMap<CompanyCreationDTO, CarSellerCompany>().ReverseMap();
            CreateMap<CarCreationDTO, Car>().ReverseMap();
            CreateMap<CarUpdateDTO, Car>().ReverseMap();
            CreateMap<CarDTO, Car>().ReverseMap();
            CreateMap<CarModelDTO, CarModel>().ReverseMap();
            CreateMap<CarModelCreationDTO, CarModel>().ReverseMap();
            CreateMap<ManufacturerDTO, Manufacturer>().ReverseMap();
            CreateMap<ManufacturerCreationDTO, Manufacturer>().ReverseMap();
        }
    }
}

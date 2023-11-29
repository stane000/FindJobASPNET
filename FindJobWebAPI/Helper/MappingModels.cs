using FindJobWebAPI.Dto;
using FindJobWebAPI.Models;

using AutoMapper;

namespace FindJobWebAPI.Helper
{
    public class MappingModels: Profile
    {

        public MappingModels()
        {
            CreateMap<Job, JobDto>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyAvgSalary, opt => opt.MapFrom(src => src.Company.AvgSalary))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.ToString()))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience))
                .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.ToString()));
            CreateMap<Worker, WorkerDto>().ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null));
            CreateMap<Company, CompanyDto>();
        }

    }
}

using AutoMapper;
using Costa.Entities;
using Costa.Models;

namespace Costa.MappingProfiles
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeViewModelId));
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.EmployeeViewModelId, opt => opt.MapFrom(src => src.Id));
        }
    }
}

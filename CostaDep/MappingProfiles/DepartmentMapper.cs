using AutoMapper;
using Costa.Entities;
using Costa.Models;

namespace Costa.MappingProfiles
{
    public class DepartmentMapper : Profile
    {
        public DepartmentMapper()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap()
                .ForMember(src => src.DepartmentViewModelId, opt => opt.MapFrom(dest => dest.Id);   
        }
    }
}

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
                .ForMember(src => src.DepartmentViewModelId, opt => opt.MapFrom(dest => dest.Id))
                //.ForMember(td => td.Date, opt => opt.MapFrom(tbm => DateOnly.FromDateTime(tbm.Date)))
                //.ForMember(td => td.Time, opt => opt.MapFrom(tbm => TimeOnly.FromDateTime(tbm.Date)))
                //.ForMember(td => td.Amount, opt => opt.MapFrom(tbm => !tbm.TransactionType.IsPositiveOperation ? 
                //decimal.Negate(tbm.Amount) : tbm.Amount))
                ;   
        }
    }
}

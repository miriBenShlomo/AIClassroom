using AutoMapper;
using AIClassroom.DAL.Models;
using AIClassroom.BL.ModelsDTO;

namespace AIClassroom.BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<User, UserDto>().ReverseMap();

     
            CreateMap<Category, CategoryDto>().ReverseMap();

           
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();

            
            CreateMap<Prompt, PromptDto>()
                .ForMember(dest => dest.PromptText, opt => opt.MapFrom(src => src.Prompt1))
                .ReverseMap()
                .ForMember(dest => dest.Prompt1, opt => opt.MapFrom(src => src.PromptText));
        }
    }
}

using AutoMapper;
using BlogWebApi.DTO;
using BlogWebApi.Models;

namespace BlogWebApi.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostDTO, MstPost>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForSourceMember(src => src.File, opt => opt.DoNotValidate());


        }
    }
}

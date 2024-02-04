using AutoMapper;
using ProfileViewer.Domain.DTOs.Auth;
using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Entities;

namespace ProfileViewer.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<EditUserDto, User>().ForMember(x => x.CreationDate, options => options.Ignore())
                .ForMember(x => x.ModificationDate, options => options.MapFrom(b => DateTime.Now));
            
            CreateMap<User, ListUserDto>();
            
            CreateMap<RegisterDto, UserFiltersDto>();
            
            CreateMap<RegisterDto, User>();

            CreateMap<LoginDto, UserFiltersDto>();

        }
    }
}

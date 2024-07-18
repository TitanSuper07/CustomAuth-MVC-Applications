using AutoMapper;
using CustomAuth3.Models;

namespace CustomAuth3.MappingProfiles
{
    public class EditUserViewModelToUserMappingProfile : Profile
    {
        public EditUserViewModelToUserMappingProfile() 
        {
            CreateMap<User, EditUserViewModel>().ReverseMap();

        }
    }
}

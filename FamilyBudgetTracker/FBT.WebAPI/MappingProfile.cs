namespace FBT.WebAPI
{
    using AutoMapper;
    using Data.Models;
    using Features.Identity;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>();
        }
    }
}

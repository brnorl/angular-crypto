using AutoMapper;
using back.Models;

namespace back.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, CreateUserModel>();
            CreateMap<CreateUserModel, User>();

            CreateMap<Btc, CreateBtcModel>();
            CreateMap<CreateBtcModel, Btc>();
        }
    }
}
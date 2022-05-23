using AutoMapper;
using NetCoreTest.Entities;
using NetCoreTest.ReqModels;

namespace NetCoreTest.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterReqModel, User>();
        }
    }
}
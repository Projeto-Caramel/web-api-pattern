using AutoMapper;
using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Entities.Models.Request;

namespace Caramel.Pattern.Services.Domain.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PetRequest, Pet>(); 
            CreateMap<PartnerRequest, PartnerRequest>(); 
        }
    }
}

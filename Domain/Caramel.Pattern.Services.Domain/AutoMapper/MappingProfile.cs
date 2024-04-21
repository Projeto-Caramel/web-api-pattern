using AutoMapper;
using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Entities.Models.Request;

namespace Caramel.Pattern.Services.Domain.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PetRequest, Pet>()
                .ForMember(x => x.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name)) 
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description)) 
                .ForMember(x => x.Castrated, opt => opt.MapFrom(src => src.Castrated)) 
                .ForMember(x => x.Vaccinated, opt => opt.MapFrom(src => src.Vaccinated)) 
                .ForMember(x => x.Age, opt => opt.MapFrom(src => src.Age)) 
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status)); 

            CreateMap<PartnerRequest, Partner>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Cnpj, opt => opt.MapFrom(src => src.Cnpj))
                .ForMember(x => x.AdoptionRate, opt => opt.MapFrom(src => src.AdoptionRate));
        }
    }
}

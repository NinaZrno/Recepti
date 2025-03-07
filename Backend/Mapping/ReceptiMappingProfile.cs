using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;

namespace Backend.Mapping
{
    public class ReceptiMappingProfile: Profile
    {

        public ReceptiMappingProfile()
        {
            CreateMap<Recept, ReceptDTORead>();
            CreateMap<ReceptDTOInsertUpdate, Recept>();
            CreateMap<Recept, ReceptDTOInsertUpdate>();


            CreateMap<Sastojak, SastojakDTORead>();
            CreateMap<SastojakDTOInsertUpdate, Sastojak>();
            CreateMap<Sastojak, SastojakDTOInsertUpdate>();

            CreateMap<Recept, ReceptDTORead>()
                .ForMember(
                    dest => dest.Sifra,
                    opt => opt.MapFrom(src => src.Sifra)
                )
                .ForMember(
                    dest => dest.Naziv,
                    opt => opt.MapFrom(src => src.Naziv)
                )
                .ForMember(
                    dest => dest.Vrsta,
                    opt => opt.MapFrom(src => src.Vrsta)
                )
                .ForMember(
                    dest => dest.Uputa,
                    opt => opt.MapFrom(src => src.Uputa)
                )
                .ForMember(
                    dest => dest.Trajanje,
                    opt => opt.MapFrom(src => src.Trajanje)
                );
        }



    }
}

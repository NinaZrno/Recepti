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

        }



    }
}

namespace Backend.Models.DTO
{
    public record SastojakDTORead(
        int Sifra,
        string Naziv,
        string? MjernaJedinica,
        string Podrijetlo,
        decimal Energija,
        decimal Ugljikohidrati,
        decimal Masti,
         decimal ZasiceniSeceri,
        decimal Vlakna,
        decimal Bjelancevine,
        decimal Sol
        );
    
    
}

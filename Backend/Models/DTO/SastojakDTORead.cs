namespace Backend.Models.DTO
{
    public record SastojakDTORead(
        int Naziv,
        string Podrijetlo,
        decimal Energija,
        decimal Ugljikohidrati,
        decimal Masti,
        decimal Vlakna,
        decimal Bjelancevine,
        decimal Sol
        );
    
    
}

using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record SastojakDTOInsertUpdate(
        [Required(ErrorMessage = "Naziv obavezno")]
        string Naziv,
        string MjernaJedinica,
        [Required(ErrorMessage = "Podrijetlo obavezno")]
        string Podrijetlo,
        [Required(ErrorMessage = "Energija obavezno")]
        decimal Energija,
        [Required(ErrorMessage = "Ugljikohidrati obavezno")]
        decimal Ugljikohidrati,
        [Required(ErrorMessage = "Masti obavezno")]
        decimal Masti,
        decimal ZasiceniSeceri,
        [Required(ErrorMessage = "Vlakna obavezno")]
        decimal Vlakna,
        [Required(ErrorMessage = "Bjelancevine obavezno")]
        decimal Bjelancevine,
        [Required(ErrorMessage = "Sol obavezno")]
        decimal Sol
        );
    
    
}

using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record ReceptDTOInsertUpdate(
        [Required(ErrorMessage = "Naziv obavezno")]
        string Naziv,
        [Required( ErrorMessage = "Vrsta je obavezna")]
        string Vrsta,
        string Uputa,
        int Trajanje
        );
    
    
}

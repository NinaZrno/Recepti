using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record ReceptDTOInsertUpdate(
        [Required(ErrorMessage = "Naziv obavezno")]
        string Naziv,
        [Range(0, 10000, ErrorMessage = "Vrijednost {0} mora biti između {1} i {2}")]
        string Vrsta,
        string Uputa,
        int Trajanje
        );
    
    
}

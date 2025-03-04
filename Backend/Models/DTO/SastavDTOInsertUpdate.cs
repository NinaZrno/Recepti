using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record SastavDTOInsertUpdate(
       [Required(ErrorMessage = "Ime obavezno")]
        int Recept,
        [Required(ErrorMessage = "Sastojak obavezan")]
        string Sastojak,
        [Required(ErrorMessage = "Napomena obavezna")]
        decimal Kolicina,
        string Napomena);
    
    
}

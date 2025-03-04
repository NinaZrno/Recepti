namespace Backend.Models.DTO
{
    public record SastavDTORead(
       int Sifra,
       int Recept,
       string Sastojak,
         decimal Kolicina,
            string Napomena
       );
    
    
}

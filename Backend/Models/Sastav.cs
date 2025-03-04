namespace Backend.Models
{
    public class Sastav : Entitet
    {
       
        public int Recept { get; set; }
        public string? Sastojak { get; set; }
        public decimal? Kolicina { get; set; }
        public string? Napomena { get; set; }




    }
}

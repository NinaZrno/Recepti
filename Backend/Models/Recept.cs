namespace Backend.Models
{
    public class Recept 
    {
        public int? Sifra { get; set; }
        public string Naziv { get; set; }
        public string Vrsta { get; set; }
        public string Uputa { get; set; }
        public Timer Trajanje { get; set; }
    }
}

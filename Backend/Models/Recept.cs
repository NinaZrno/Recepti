namespace Backend.Models
{
    public class Recept : Entitet
    {
        public string Naziv { get; set; }
        public string Vrsta { get; set; }
        public string Uputa { get; set; }
        public int Trajanje { get; set; }
    }
}

namespace Backend.Models
{
    public class Sastojak: Entitet
    {
        public int Naziv { get; set; }
        public string Podrijetlo { get; set; } = "";
        public  decimal Energija { get; set; }
        public decimal Ugljikohidrati { get; set; }
        public decimal Masti { get; set; }
        public decimal Vlakna { get; set; }
        public decimal Bjelancevine { get; set; }
        public decimal Sol { get; set; }

    }
}

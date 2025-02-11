namespace Backend.Models
{
    internal class Sastojak : Recept
    {
       public int? Sifra { get; set; }
        public string Naziv { get; set; }
        public string Podrijetlo { get; set; }
        public string Mjera { get; set; }
        public decimal Energija { get; set; }
        public decimal Ugljikohidrati { get; set; }
        public decimal Masti { get; set; }
        public decimal Zasiceni_seceri { get; set; }
        public decimal Vlakna { get; set; }
        public decimal Bjelancevine { get; set; }
        public decimal Sol { get; set; }





    }
}

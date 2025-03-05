using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Sastojak: Entitet
    {
        public string Naziv { get; set; } = "";
        [Column("mjerna_jedinica")]
        public string? MjernaJedinica { get; set; } 
        public string Podrijetlo { get; set; } = "";
        public  decimal Energija { get; set; }
        public decimal Ugljikohidrati { get; set; }
        public decimal Masti { get; set; }
        [Column("zasiceni_seceri")]
        public decimal ZasiceniSeceri { get; set; }
        public decimal Vlakna { get; set; }
        public decimal Bjelancevine { get; set; }
        public decimal Sol { get; set; }

    }
}

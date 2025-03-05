using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Sastav : Entitet
    {
        [ForeignKey("recept")]
        public required Recept Recept { get; set; }
        [ForeignKey("sastojak")]
        public required Sastojak Sastojak { get; set; }
        public decimal Kolicina { get; set; }
        public string Napomena { get; set; } = "";




    }
}

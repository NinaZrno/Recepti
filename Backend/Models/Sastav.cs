namespace Backend.Models
{
    
    public class Sastav : Recept
    {

        public int? Sifra { get; set; }
        public string Naziv { get; set; }

        public string Sastojak { get; set; }

        public decimal Kolicina { get; set; }

        public string Napomena { get; set; }


    }
}

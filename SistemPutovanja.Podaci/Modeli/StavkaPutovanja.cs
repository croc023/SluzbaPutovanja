using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka.Modeli
{
    [Table("StavkePutovanja")]
    public class StavkaPutovanja
    {
        [Key]
        public int StavkaID { get; set; }
        public int BrojZahteva { get; set; }
        public string VrstaTroska { get; set; }
        public decimal Iznos { get; set; }

        [ForeignKey("BrojZahteva")]
        public virtual ZahtevZaPutovanje Zahtev { get; set; }
    }
}
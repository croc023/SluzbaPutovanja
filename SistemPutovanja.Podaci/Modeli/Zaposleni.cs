using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka.Modeli
{
    [Table("Zaposleni")]
    public class Zaposleni
    {
        [Key]
        public int ZaposleniID { get; set; }
        public string ImePrezime { get; set; }
        public string Sektor { get; set; }
    }
}
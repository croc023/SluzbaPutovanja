using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka.Modeli
{
    [Table("Korisnici")]
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }

        [Required]
        [StringLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [StringLength(255)]
        public string Lozinka { get; set; }

        [Required]
        [StringLength(30)]
        public string Uloga { get; set; }
    }
}
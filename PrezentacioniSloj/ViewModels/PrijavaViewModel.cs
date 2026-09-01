using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.ViewModels
{
    public class PrijavaViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }
    }
}
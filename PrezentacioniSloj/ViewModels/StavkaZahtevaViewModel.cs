using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.ViewModels
{
    public class StavkaZahtevaViewModel
    {
        public int StavkaID { get; set; }
        public int ZahtevID { get; set; }

        [Required(ErrorMessage = "Opis troška je obavezan.")]
        [Display(Name = "Opis troška")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Iznos je obavezan.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Iznos mora biti veći od nule.")]
        [Display(Name = "Iznos (RSD)")]
        public decimal Iznos { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PrezentacioniSloj.ViewModels
{
    public class KreiranjeZahtevaViewModel
    {
        public int BrojZahteva { get; set; }

        [Display(Name = "Oznaka zahteva")]
        public string OznakaZahteva { get; set; }

        [Required(ErrorMessage = "Izbor zaposlenog je obavezan.")]
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati validnog zaposlenog iz liste.")]
        [Display(Name = "Zaposleni")]
        public int ZaposleniID { get; set; }

        [Required(ErrorMessage = "Destinacija je obavezna.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Destinacija mora imati između 2 i 100 karaktera.")]
        [Display(Name = "Destinacija")]
        public string Destinacija { get; set; }

        [StringLength(250, ErrorMessage = "Cilj agende ne može prelaziti 250 karaktera.")]
        [Display(Name = "Cilj agende")]
        public string CiljAgende { get; set; }

        [Required(ErrorMessage = "Poslovni razlog je obavezan.")]
        [StringLength(500, ErrorMessage = "Poslovni razlog ne može prelaziti 500 karaktera.")]
        [Display(Name = "Poslovni razlog")]
        public string PoslovniRazlog { get; set; }

        [StringLength(200, ErrorMessage = "Uticaj na budžet ne može prelaziti 200 karaktera.")]
        [Display(Name = "Uticaj na budžet")]
        public string UticajNaBudzet { get; set; }

        [Required(ErrorMessage = "Ukupan procenjeni trošak je obavezan.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Procenjeni trošak mora biti u opsegu od 0.01 do 1.000.000,00 RSD.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ukupan procenjeni trošak (RSD)")]
        public decimal UkupanProcenjeniTrosak { get; set; }

        public IEnumerable<SelectListItem> ZaposleniLista { get; set; }
    }
}
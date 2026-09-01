using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka.Modeli
{
    [Table("ZahteviZaPutovanje")]
    public class ZahtevZaPutovanje
    {
        [Key]
        public int BrojZahteva { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string OznakaZahteva { get; set; }
        public DateTime DatumPodnosenja { get; set; }
        public int ZaposleniID { get; set; }
        public string Destinacija { get; set; }
        public string CiljAgende { get; set; }
        public string PoslovniRazlog { get; set; }
        public string Status { get; set; }
        public decimal UkupanProcenjeniTrosak { get; set; }
        public string UticajNaBudzet { get; set; }
        public bool PotvrdaDirektora { get; set; }
        public int? KorisnikDirektorID { get; set; }

        [ForeignKey("ZaposleniID")]
        public virtual Zaposleni Zaposleni { get; set; }

        [ForeignKey("KorisnikDirektorID")]
        public virtual Korisnik KorisnikDirektor { get; set; }

        public virtual ICollection<StavkaPutovanja> Stavke { get; set; }
    }
}
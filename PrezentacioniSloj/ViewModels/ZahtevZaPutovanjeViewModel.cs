using System;
using System.Collections.Generic;

namespace PrezentacioniSloj.ViewModels
{
    public class ZahtevZaPutovanjeViewModel
    {
        public int BrojZahteva { get; set; }
        public string OznakaZahteva { get; set; }
        public DateTime DatumPodnosenja { get; set; }
        public int ZaposleniID { get; set; }
        public string ImePrezimeZaposlenog { get; set; }
        public string Destinacija { get; set; }
        public string CiljAgende { get; set; }
        public string PoslovniRazlog { get; set; }
        public string Status { get; set; }
        public decimal UkupanProcenjeniTrosak { get; set; }
        public string UticajNaBudzet { get; set; }
        public bool PotvrdaDirektora { get; set; }

        public List<StavkaPutovanjaViewModel> Stavke { get; set; } = new List<StavkaPutovanjaViewModel>();
    }

    public class StavkaPutovanjaViewModel
    {
        public int StavkaID { get; set; }
        public string VrstaTroska { get; set; }
        public decimal Iznos { get; set; }
    }
}
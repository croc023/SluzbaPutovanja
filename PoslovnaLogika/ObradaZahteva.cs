using Newtonsoft.Json.Linq;
using SlojPodataka.Modeli;
using SlojPodataka.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace PoslovnaLogika
{
    public class ObradaZahteva
    {
        private readonly ZahtevZaPutovanjeRepo _zahtevRepo = new ZahtevZaPutovanjeRepo();
        private readonly ZaposleniRepo _zaposleniRepo = new ZaposleniRepo();

        public List<ZahtevZaPutovanje> PreuzmiSveZahteve()
        {
            return _zahtevRepo.PreuzmiSve();
        }

        public ZahtevZaPutovanje VratiZahtevPoId(int id)
        {
            return _zahtevRepo.VratiPoId(id);
        }

        public List<Zaposleni> VratiSveZaposlene()
        {
            return _zaposleniRepo.PreuzmiSve();
        }

        public decimal UcitajLimitIzJsona(string putanjaDoJson)
        {
            try
            {
                if (!string.IsNullOrEmpty(putanjaDoJson) && File.Exists(putanjaDoJson))
                {
                    string jsonSadrzaj = File.ReadAllText(putanjaDoJson);
                    JObject jsonObjekat = JObject.Parse(jsonSadrzaj);

                    var limitToken = jsonObjekat["pravila_odobrenja"]?["limit_troska_za_odobrenje"];
                    if (limitToken != null)
                    {
                        return Convert.ToDecimal(limitToken);
                    }
                }
            }
            catch (Exception)
            {
            }
            return 500m;
        }

        public void KreirajNoviZahtev(ZahtevZaPutovanje zahtev, string putanjaDoJson = null)
        {
            zahtev.DatumPodnosenja = DateTime.Now;
            zahtev.PotvrdaDirektora = false;

            decimal limit = UcitajLimitIzJsona(putanjaDoJson);

            if (zahtev.UkupanProcenjeniTrosak > limit)
            {
                zahtev.Status = "Na čekanju";
            }
            else
            {
                zahtev.Status = "Odobren";
            }

            int sledeciId = _zahtevRepo.PreuzmiSve().Count + 1;
            zahtev.OznakaZahteva = $"TR-{DateTime.Now.Year}-{sledeciId:D4}";

            _zahtevRepo.Ubaci(zahtev);
        }

        public void IzmeniZahtev(ZahtevZaPutovanje izmenjenZahtev)
        {
            var postojeci = _zahtevRepo.VratiPoId(izmenjenZahtev.BrojZahteva);
            if (postojeci != null)
            {
                postojeci.ZaposleniID = izmenjenZahtev.ZaposleniID;
                postojeci.Destinacija = izmenjenZahtev.Destinacija;
                postojeci.CiljAgende = izmenjenZahtev.CiljAgende;
                postojeci.PoslovniRazlog = izmenjenZahtev.PoslovniRazlog;
                postojeci.UkupanProcenjeniTrosak = izmenjenZahtev.UkupanProcenjeniTrosak;
                postojeci.UticajNaBudzet = izmenjenZahtev.UticajNaBudzet;

                _zahtevRepo.Izmeni(postojeci);
            }
        }

        public bool OdobriZahtev(int id, int? direktorId = null)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev == null) return false;

            zahtev.Status = "Odobren";
            zahtev.PotvrdaDirektora = true;
            if (direktorId.HasValue)
            {
                zahtev.KorisnikDirektorID = direktorId;
            }

            _zahtevRepo.Izmeni(zahtev);
            return true;
        }

        public bool OdbijZahtev(int id, int? direktorId = null, string razlog = null)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev == null) return false;

            zahtev.Status = "Odbijen";
            zahtev.PotvrdaDirektora = false;
            if (direktorId.HasValue)
            {
                zahtev.KorisnikDirektorID = direktorId;
            }

            _zahtevRepo.Izmeni(zahtev);
            return true;
        }

        public bool ObrisiZahtev(int id)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev == null) return false;

            _zahtevRepo.Obrisi(id);
            return true;
        }
    }
}
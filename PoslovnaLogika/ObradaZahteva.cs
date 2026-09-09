using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using SlojPodataka.Modeli;
using SlojPodataka.Repositories;
using SistemPutovanja.Repozitorijumi;

namespace PoslovnaLogika
{
    public class ObradaZahteva
    {
        private readonly ZahtevZaPutovanjeRepozitorijum _zahtevRepo;
        private readonly ZaposleniRepo _zaposleniRepo;

        public ObradaZahteva()
        {
            _zahtevRepo = new ZahtevZaPutovanjeRepozitorijum();
            _zaposleniRepo = new ZaposleniRepo();
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

        public bool ProveriOgranicenjeTroska(decimal ukupniTrosak, string putanjaDoJson)
        {
            decimal limit = UcitajLimitIzJsona(putanjaDoJson);
            return ukupniTrosak <= limit;
        }

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

        public void KreirajNoviZahtev(ZahtevZaPutovanje zahtev)
        {
            _zahtevRepo.Ubaci(zahtev);
        }

        public void ObrisiZahtev(int id)
        {
            _zahtevRepo.Obrisi(id);
        }

        public void OdobriZahtev(int id)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev != null)
            {
                zahtev.Status = "Odobren";
                _zahtevRepo.Izmeni(zahtev);
            }
        }

        public void OdbijZahtev(int id)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev != null)
            {
                zahtev.Status = "Odbijen";
                _zahtevRepo.Izmeni(zahtev);
            }
        }

        public bool OdobriZahtev(int id, int direktorId)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev == null)
            {
                return false;
            }

            zahtev.Status = "Odobren";
            _zahtevRepo.Izmeni(zahtev);
            return true;
        }

        public bool OdbijZahtev(int id, int direktorId, string obrazlozenje)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev == null)
            {
                return false;
            }

            zahtev.Status = "Odbijen";
            _zahtevRepo.Izmeni(zahtev);
            return true;
        }

        public void IzmeniZahtev(ZahtevZaPutovanje zahtev)
        {
            _zahtevRepo.Izmeni(zahtev);
        }
    }
}
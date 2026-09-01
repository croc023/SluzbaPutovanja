using PoslovnaLogika;
using PrezentacioniSloj.ViewModels;
using SlojPodataka.Modeli;
using SlojPodataka.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PrezentacioniSloj.Controllers
{
    public class ZahtevZaPutovanjeController : Controller
    {
        private readonly ObradaZahteva _obradaZahteva = new ObradaZahteva();
        private readonly ZaposleniRepo _zaposleniRepo = new ZaposleniRepo();

        public ActionResult Indeks(string status, string pretraga)
        {
            var zahtevi = _obradaZahteva.PreuzmiSveZahteve();

            if (!string.IsNullOrEmpty(status))
            {
                zahtevi = zahtevi.Where(z => z.Status == status).ToList();
            }

            if (!string.IsNullOrEmpty(pretraga))
            {
                string p = pretraga.ToLower();
                zahtevi = zahtevi.Where(z =>
                    (z.Destinacija != null && z.Destinacija.ToLower().Contains(p)) ||
                    (z.OznakaZahteva != null && z.OznakaZahteva.ToLower().Contains(p)) ||
                    (z.Zaposleni != null && z.Zaposleni.ImePrezime != null && z.Zaposleni.ImePrezime.ToLower().Contains(p))
                ).ToList();
            }

            var model = zahtevi.Select(z => new ZahtevZaPutovanjeViewModel
            {
                BrojZahteva = z.BrojZahteva,
                OznakaZahteva = z.OznakaZahteva,
                DatumPodnosenja = z.DatumPodnosenja,
                ImePrezimeZaposlenog = z.Zaposleni != null ? z.Zaposleni.ImePrezime : "/",
                Destinacija = z.Destinacija,
                Status = z.Status,
                UkupanProcenjeniTrosak = z.UkupanProcenjeniTrosak
            }).ToList();

            ViewBag.StatusFilter = status;
            ViewBag.PretragaFilter = pretraga;

            return View(model);
        }

        public ActionResult Detalji(int id)
        {
            var zahtev = _obradaZahteva.VratiZahtevPoId(id);
            if (zahtev == null) return HttpNotFound();

            var model = new ZahtevZaPutovanjeViewModel
            {
                BrojZahteva = zahtev.BrojZahteva,
                OznakaZahteva = zahtev.OznakaZahteva,
                DatumPodnosenja = zahtev.DatumPodnosenja,
                ImePrezimeZaposlenog = zahtev.Zaposleni != null ? zahtev.Zaposleni.ImePrezime : "/",
                Destinacija = zahtev.Destinacija,
                CiljAgende = zahtev.CiljAgende,
                PoslovniRazlog = zahtev.PoslovniRazlog,
                Status = zahtev.Status,
                UticajNaBudzet = zahtev.UticajNaBudzet,
                PotvrdaDirektora = zahtev.PotvrdaDirektora,
                UkupanProcenjeniTrosak = zahtev.UkupanProcenjeniTrosak,
                Stavke = zahtev.Stavke != null ? zahtev.Stavke.Select(s => new StavkaPutovanjaViewModel
                {
                    StavkaID = s.StavkaID,
                    VrstaTroska = s.VrstaTroska,
                    Iznos = s.Iznos
                }).ToList() : new List<StavkaPutovanjaViewModel>()
            };

            return View(model);
        }

        public ActionResult Kreiraj()
        {
            var model = new KreiranjeZahtevaViewModel
            {
                ZaposleniLista = _obradaZahteva.VratiSveZaposlene().Select(z => new SelectListItem
                {
                    Value = z.ZaposleniID.ToString(),
                    Text = z.ImePrezime
                })
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Izmeni(int id)
        {
            var zahtev = _obradaZahteva.VratiZahtevPoId(id);
            if (zahtev == null)
            {
                return HttpNotFound();
            }

            var zaposleni = _obradaZahteva.VratiSveZaposlene();

            var model = new KreiranjeZahtevaViewModel
            {
                BrojZahteva = zahtev.BrojZahteva,
                ZaposleniID = zahtev.ZaposleniID,
                Destinacija = zahtev.Destinacija,
                CiljAgende = zahtev.CiljAgende,
                PoslovniRazlog = zahtev.PoslovniRazlog,
                UkupanProcenjeniTrosak = zahtev.UkupanProcenjeniTrosak,
                UticajNaBudzet = zahtev.UticajNaBudzet,
                ZaposleniLista = zaposleni.Select(z => new SelectListItem
                {
                    Value = z.ZaposleniID.ToString(),
                    Text = z.ImePrezime,
                    Selected = z.ZaposleniID == zahtev.ZaposleniID
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kreiraj(KreiranjeZahtevaViewModel model)
        {
            bool vecPostoji = _obradaZahteva.PreuzmiSveZahteve().Any(z =>
                z.ZaposleniID == model.ZaposleniID &&
                z.Destinacija != null &&
                z.Destinacija.Trim().ToLower() == (model.Destinacija ?? "").Trim().ToLower() &&
                z.DatumPodnosenja == DateTime.Now.Date);

            if (vecPostoji)
            {
                ModelState.AddModelError("", "Zahtev za izabranog zaposlenog i navedenu destinaciju već je podnet za današnji datum.");
            }

            if (ModelState.IsValid)
            {
                var noviZahtev = new ZahtevZaPutovanje
                {
                    ZaposleniID = model.ZaposleniID,
                    Destinacija = model.Destinacija,
                    CiljAgende = model.CiljAgende,
                    PoslovniRazlog = model.PoslovniRazlog,
                    UticajNaBudzet = model.UticajNaBudzet,
                    UkupanProcenjeniTrosak = model.UkupanProcenjeniTrosak,
                    DatumPodnosenja = DateTime.Now.Date,
                    Status = "Na čekanju",
                    PotvrdaDirektora = false,
                    OznakaZahteva = $"TR-{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}"
                };

                _obradaZahteva.KreirajNoviZahtev(noviZahtev);

                return RedirectToAction("Indeks");
            }

            model.ZaposleniLista = _obradaZahteva.VratiSveZaposlene().Select(z => new SelectListItem
            {
                Value = z.ZaposleniID.ToString(),
                Text = z.ImePrezime
            });

            return View(model);
        }

        public ActionResult Obrisi(int id)
        {
            var zahtev = _obradaZahteva.VratiZahtevPoId(id);
            if (zahtev == null)
            {
                return HttpNotFound();
            }

            var model = new ZahtevZaPutovanjeViewModel
            {
                BrojZahteva = zahtev.BrojZahteva,
                OznakaZahteva = zahtev.OznakaZahteva,
                DatumPodnosenja = zahtev.DatumPodnosenja,
                ImePrezimeZaposlenog = zahtev.Zaposleni != null ? zahtev.Zaposleni.ImePrezime : "/",
                Destinacija = zahtev.Destinacija,
                Status = zahtev.Status,
                UkupanProcenjeniTrosak = zahtev.UkupanProcenjeniTrosak
            };

            return View(model);
        }

        [HttpPost, ActionName("Obrisi")]
        [ValidateAntiForgeryToken]
        public ActionResult ObrisiPotvrđeno(int id)
        {
            var zahtev = _obradaZahteva.VratiZahtevPoId(id);
            if (zahtev != null)
            {
                _obradaZahteva.ObrisiZahtev(id);
            }

            return RedirectToAction("Indeks");
        }

        public ActionResult StampaSpiska(string status, string pretraga)
        {
            var zahtevi = _obradaZahteva.PreuzmiSveZahteve();

            if (!string.IsNullOrEmpty(status))
            {
                zahtevi = zahtevi.Where(z => z.Status == status).ToList();
            }

            if (!string.IsNullOrEmpty(pretraga))
            {
                string p = pretraga.ToLower();
                zahtevi = zahtevi.Where(z =>
                    (z.Destinacija != null && z.Destinacija.ToLower().Contains(p)) ||
                    (z.OznakaZahteva != null && z.OznakaZahteva.ToLower().Contains(p)) ||
                    (z.Zaposleni != null && z.Zaposleni.ImePrezime != null && z.Zaposleni.ImePrezime.ToLower().Contains(p))
                ).ToList();
            }

            var model = zahtevi.Select(z => new ZahtevZaPutovanjeViewModel
            {
                BrojZahteva = z.BrojZahteva,
                OznakaZahteva = z.OznakaZahteva,
                DatumPodnosenja = z.DatumPodnosenja,
                ImePrezimeZaposlenog = z.Zaposleni != null ? z.Zaposleni.ImePrezime : "/",
                Destinacija = z.Destinacija,
                Status = z.Status,
                UkupanProcenjeniTrosak = z.UkupanProcenjeniTrosak
            }).ToList();

            ViewBag.StatusFilter = status;
            ViewBag.PretragaFilter = pretraga;

            return View(model);
        }

        public ActionResult StampaDokumenta(int id)
        {
            var zahtev = _obradaZahteva.VratiZahtevPoId(id);
            if (zahtev == null) return HttpNotFound();

            var model = new ZahtevZaPutovanjeViewModel
            {
                BrojZahteva = zahtev.BrojZahteva,
                OznakaZahteva = zahtev.OznakaZahteva,
                DatumPodnosenja = zahtev.DatumPodnosenja,
                ImePrezimeZaposlenog = zahtev.Zaposleni != null ? zahtev.Zaposleni.ImePrezime : "/",
                Destinacija = zahtev.Destinacija,
                CiljAgende = zahtev.CiljAgende,
                PoslovniRazlog = zahtev.PoslovniRazlog,
                Status = zahtev.Status,
                UticajNaBudzet = zahtev.UticajNaBudzet,
                PotvrdaDirektora = zahtev.PotvrdaDirektora,
                UkupanProcenjeniTrosak = zahtev.UkupanProcenjeniTrosak,
                Stavke = zahtev.Stavke != null ? zahtev.Stavke.Select(s => new StavkaPutovanjaViewModel
                {
                    StavkaID = s.StavkaID,
                    VrstaTroska = s.VrstaTroska,
                    Iznos = s.Iznos
                }).ToList() : new List<StavkaPutovanjaViewModel>()
            };

            return View(model);
        }

        public ActionResult StampaObrazac(int id)
        {
            var zahtev = _obradaZahteva.VratiZahtevPoId(id);
            if (zahtev == null)
            {
                return HttpNotFound();
            }

            var model = new ZahtevZaPutovanjeViewModel
            {
                BrojZahteva = zahtev.BrojZahteva,
                OznakaZahteva = zahtev.OznakaZahteva,
                ImePrezimeZaposlenog = zahtev.Zaposleni != null ? zahtev.Zaposleni.ImePrezime : "",
                Destinacija = zahtev.Destinacija,
                DatumPodnosenja = zahtev.DatumPodnosenja,
                Status = zahtev.Status,
                PoslovniRazlog = zahtev.PoslovniRazlog,
                CiljAgende = zahtev.CiljAgende,
                UkupanProcenjeniTrosak = zahtev.UkupanProcenjeniTrosak,
                PotvrdaDirektora = zahtev.PotvrdaDirektora,
                Stavke = zahtev.Stavke?.Select(s => new StavkaPutovanjaViewModel { StavkaID = s.StavkaID, VrstaTroska = s.VrstaTroska, Iznos = s.Iznos }).ToList()
            };

            return View(model);
        }
    }
}
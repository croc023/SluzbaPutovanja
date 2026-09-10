using PrezentacioniSloj.ViewModels;
using SlojPodataka.Repositories;
using System.Web.Mvc;
using System.Web.Security;

namespace PrezentacioniSloj.Controllers
{
    public class PrijavaController : Controller
    {
        private readonly KorisnikRepozitorijum _korisnikRepo = new KorisnikRepozitorijum();

        [HttpGet]
        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prijava(PrijavaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var korisnik = _korisnikRepo.ProveriKorisnika(model.KorisnickoIme, model.Lozinka);
            if (korisnik != null)
            {
                FormsAuthentication.SetAuthCookie(korisnik.KorisnickoIme, false);
                Session["KorisnikID"] = korisnik.KorisnikID;
                Session["KorisnickoIme"] = korisnik.KorisnickoIme;
                Session["Uloga"] = korisnik.Uloga;

                return RedirectToAction("Indeks", "ZahtevZaPutovanje");
            }

            ModelState.AddModelError("", "Neispravno korisničko ime ili lozinka.");
            return View(model);
        }

        public ActionResult Odjava()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Prijava");
        }
    }
}
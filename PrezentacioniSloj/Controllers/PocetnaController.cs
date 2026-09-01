using System.Web.Mvc;

namespace PrezentacioniSloj.Controllers
{
    public class PocetnaController : Controller
    {
        public ActionResult Pocetna()
        {
            return RedirectToAction("Indeks", "ZahtevZaPutovanje");
        }

        public ActionResult ONama()
        {
            return View("ONama");
        }

        public ActionResult Kontakt()
        {
            return View();
        }
    }
}
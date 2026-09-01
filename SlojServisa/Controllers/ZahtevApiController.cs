using System;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;
using PoslovnaLogika;
using SlojPodataka.Repositories;
using SlojPodataka.Modeli;

namespace SlojServisa.Controllers
{
    [RoutePrefix("api/zahtevi")]
    public class ZahtevApiController : ApiController
    {
        private readonly ZahtevZaPutovanjeRepo _zahtevRepo = new ZahtevZaPutovanjeRepo();
        private readonly ObradaZahteva _obradaZahteva = new ObradaZahteva();

        [HttpGet]
        [Route("")]
        public IHttpActionResult PreuzmiSve()
        {
            var zahtevi = _zahtevRepo.PreuzmiSve();
            return Ok(zahtevi);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult VratiPoId(int id)
        {
            var zahtev = _zahtevRepo.VratiPoId(id);
            if (zahtev == null)
                return NotFound();

            return Ok(zahtev);
        }

        [HttpPost]
        [Route("odobri/{id:int}")]
        public IHttpActionResult Odobri(int id, [FromBody] int direktorId)
        {
            bool uspesno = _obradaZahteva.OdobriZahtev(id, direktorId);
            if (!uspesno)
                return BadRequest("Zahtev ne postoji ili ga nije moguće odobriti.");

            return Ok(new { Poruka = "Zahtev je uspešno odobren." });
        }

        [HttpPost]
        [Route("odbij/{id:int}")]
        public IHttpActionResult Odbij(int id, [FromBody] OdbijanjeRequest zahtevZaOdbijanje)
        {
            if (zahtevZaOdbijanje == null)
                return BadRequest("Podaci za odbijanje nisu ispravni.");

            bool uspesno = _obradaZahteva.OdbijZahtev(id, zahtevZaOdbijanje.DirektorId, zahtevZaOdbijanje.Obrazlozenje);
            if (!uspesno)
                return BadRequest("Zahtev ne postoji ili ga nije moguće odbiti.");

            return Ok(new { Poruka = "Zahtev je uspešno odbijen." });
        }

        [HttpGet]
        [Route("pravila")]
        public IHttpActionResult VratiPoslovnaPravila()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/poslovna_pravila.json");
            if (!File.Exists(putanja))
                return NotFound();

            string jsonTekst = File.ReadAllText(putanja);
            return Ok(Newtonsoft.Json.JsonConvert.DeserializeObject(jsonTekst));
        }
    }

    public class OdbijanjeRequest
    {
        public int DirektorId { get; set; }
        public string Obrazlozenje { get; set; }
    }
}
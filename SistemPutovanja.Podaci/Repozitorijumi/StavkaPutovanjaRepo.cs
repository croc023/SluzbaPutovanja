using System.Collections.Generic;
using System.Linq;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repositories
{
    public class StavkaPutovanjaRepo : BazniRepozitorijum<StavkaPutovanja>
    {
        public List<StavkaPutovanja> VratiPoZahtevId(int zahtevId)
        {
            return _context.StavkePutovanja
                .Where(s => s.BrojZahteva == zahtevId)
                .ToList();
        }

        public void ObrisiPoZahtevId(int zahtevId)
        {
            var stavke = VratiPoZahtevId(zahtevId);
            if (stavke != null && stavke.Any())
            {
                _context.StavkePutovanja.RemoveRange(stavke);
                _context.SaveChanges();
            }
        }
    }
}
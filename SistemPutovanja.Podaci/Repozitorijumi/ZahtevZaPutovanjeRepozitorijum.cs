using SlojPodataka;
using SlojPodataka.Modeli;
using SlojPodataka.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SistemPutovanja.Repozitorijumi
{
    public class ZahtevZaPutovanjeRepozitorijum : BazniRepozitorijum<ZahtevZaPutovanje>
    {
        private readonly AppDbContext _context = new AppDbContext();

        public List<ZahtevZaPutovanje> PreuzmiSve()
        {
            return _context.ZahteviZaPutovanje
                .Include(z => z.Zaposleni)
                .Include(z => z.Stavke)
                .ToList();
        }

        public ZahtevZaPutovanje VratiPoId(int id)
        {
            return _context.ZahteviZaPutovanje
                .Include(z => z.Zaposleni)
                .Include(z => z.Stavke)
                .FirstOrDefault(z => z.BrojZahteva == id);
        }

        public void Ubaci(ZahtevZaPutovanje entitet)
        {
            _context.ZahteviZaPutovanje.Add(entitet);
            _context.SaveChanges();
        }

        public void Izmeni(ZahtevZaPutovanje entitet)
        {
            var postojeci = _context.ZahteviZaPutovanje.Find(entitet.BrojZahteva);
            if (postojeci != null)
            {
                _context.Entry(postojeci).CurrentValues.SetValues(entitet);
                _context.SaveChanges();
            }
        }

        public void Obrisi(int id)
        {
            using (var db = new AppDbContext())
            {
                var zahtev = db.ZahteviZaPutovanje.Find(id);
                if (zahtev != null)
                {
                    db.ZahteviZaPutovanje.Remove(zahtev);
                    db.SaveChanges();
                }
            }
        }
    }
}
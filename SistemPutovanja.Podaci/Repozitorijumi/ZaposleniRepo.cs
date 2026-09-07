using System.Collections.Generic;
using System.Linq;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repositories
{
    public class ZaposleniRepo : BazniRepozitorijum<Zaposleni>
    {
        private readonly AppDbContext _context = new AppDbContext();

        public List<Zaposleni> PreuzmiSve()
        {
            return _context.Zaposleni.ToList();
        }
    }
}
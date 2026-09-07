using System.Linq;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repositories
{
    public class KorisnikRepo : BazniRepozitorijum<Korisnik>
    {
        public Korisnik VratiPoKorisnickomImenu(string korisnickoIme)
        {
            return _context.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme);
        }

        public Korisnik ProveriKorisnika(string korisnickoIme, string lozinka)
        {
            return _context.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme && k.Lozinka == lozinka);
        }
    }
}
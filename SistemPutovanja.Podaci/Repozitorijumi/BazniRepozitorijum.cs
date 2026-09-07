using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SlojPodataka.Repositories
{
    public class BazniRepozitorijum<TEntitet> where TEntitet : class
    {
        protected readonly AppDbContext _context;

        public BazniRepozitorijum()
        {
            _context = new AppDbContext();
        }

        public List<TEntitet> PreuzmiSve()
        {
            return _context.Set<TEntitet>().ToList();
        }

        public TEntitet VratiPoId(int id)
        {
            return _context.Set<TEntitet>().Find(id);
        }

        public void Ubaci(TEntitet entitet)
        {
            _context.Set<TEntitet>().Add(entitet);
            _context.SaveChanges();
        }

        public void Izmeni(TEntitet entitet)
        {
            _context.Entry(entitet).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Obrisi(int id)
        {
            var entitet = VratiPoId(id);
            if (entitet != null)
            {
                _context.Set<TEntitet>().Remove(entitet);
                _context.SaveChanges();
            }
        }
    }
}
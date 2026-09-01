using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SlojPodataka.Repositories
{
    public class BaseRepo<T> where T : class
    {
        protected readonly AppDbContext _context = new AppDbContext();

        public List<T> PreuzmiSve()
        {
            return _context.Set<T>().ToList();
        }

        public T VratiPoId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Ubaci(T entitet)
        {
            _context.Set<T>().Add(entitet);
            _context.SaveChanges();
        }

        public void Izmeni(T entitet)
        {
            _context.Entry(entitet).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Obrisi(int id)
        {
            var entitet = VratiPoId(id);
            if (entitet != null)
            {
                _context.Set<T>().Remove(entitet);
                _context.SaveChanges();
            }
        }
    }
}
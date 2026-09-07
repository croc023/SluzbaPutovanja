using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemPutovanja.Podaci
{
    public abstract class OsnovniRepozitorijum<TEntitet> where TEntitet : class
    {
        protected readonly string KonekcijaString;

        protected OsnovniRepozitorijum(string konekcijaString)
        {
            KonekcijaString = konekcijaString;
        }

        public abstract IEnumerable<TEntitet> PreuzmiSve();
        public abstract void Izmeni(TEntitet entitet);
    }
}

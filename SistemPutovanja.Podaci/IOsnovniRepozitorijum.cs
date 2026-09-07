using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemPutovanja.Podaci
{
    public interface IOsnovniRepozitorijum<TEntitet> where TEntitet : class
    {
        IEnumerable<TEntitet> UcitajSve();
        void Dodaj(TEntitet entitet);
    }
}

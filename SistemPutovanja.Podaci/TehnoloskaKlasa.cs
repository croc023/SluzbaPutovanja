using System.Collections.Generic;

namespace SlojPodataka
{
    public abstract class TehnoloskaKlasa<TEntitet> where TEntitet : class
    {
        protected readonly SqlBroker _broker;

        protected TehnoloskaKlasa()
        {
            _broker = new SqlBroker();
        }

        public abstract void DodajProcedurom(TEntitet entitet);
        public abstract void IzmeniProcedurom(TEntitet entitet);
        public abstract void ObrisiProcedurom(int id);
        public abstract List<TEntitet> VratiSveProcedurom();
    }
}
using System.Data.Entity;
using SlojPodataka.Modeli;

namespace SlojPodataka
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=SluzbenaPutovanjaDB") { }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Zaposleni> Zaposleni { get; set; }
        public DbSet<ZahtevZaPutovanje> ZahteviZaPutovanje { get; set; }
        public DbSet<StavkaPutovanja> StavkePutovanja { get; set; }
    }
}
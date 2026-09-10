using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repositories
{
    public class ZahtevSqlRepo : TehnoloskaKlasa<ZahtevZaPutovanje>
    {
        private const string ConnectionString = "name=SluzbenaPutovanjaDB";

        public override void DodajProcedurom(ZahtevZaPutovanje entitet)
        {
            _broker.OtvoriKonekciju(ConnectionString);
            _broker.PostaviProceduru("sp_DodajZahtev");
            _broker.DodajParametar("@ZaposleniID", entitet.ZaposleniID);
            _broker.DodajParametar("@Destinacija", entitet.Destinacija);
            _broker.DodajParametar("@PoslovniRazlog", entitet.PoslovniRazlog);
            _broker.DodajParametar("@UkupanProcenjeniTrosak", entitet.UkupanProcenjeniTrosak);
            _broker.DodajParametar("@Status", entitet.Status ?? "Na cekanju");
            _broker.IzvršiIzmenu();
            _broker.ZatvoriKonekciju();
        }

        public override void IzmeniProcedurom(ZahtevZaPutovanje entitet)
        {
            // Ako je potrebna izmena zahteva preko procedure
        }

        public override void ObrisiProcedurom(int brojZahteva)
        {
            _broker.OtvoriKonekciju(ConnectionString);
            _broker.PostaviProceduru("sp_ObrisiZahtev");
            _broker.DodajParametar("@BrojZahteva", brojZahteva);
            _broker.IzvršiIzmenu();
            _broker.ZatvoriKonekciju();
        }

        public override List<ZahtevZaPutovanje> VratiSveProcedurom()
        {
            var lista = new List<ZahtevZaPutovanje>();

            _broker.OtvoriKonekciju(ConnectionString);
            _broker.PostaviProceduru("sp_VratiSveZahteve");

            using (SqlDataReader reader = _broker.IzvrsiUpit())
            {
                while (reader.Read())
                {
                    lista.Add(new ZahtevZaPutovanje
                    {
                        BrojZahteva = Convert.ToInt32(reader["BrojZahteva"]),
                        ZaposleniID = Convert.ToInt32(reader["ZaposleniID"]),
                        Destinacija = reader["Destinacija"].ToString(),
                        PoslovniRazlog = reader["PoslovniRazlog"].ToString(),
                        UkupanProcenjeniTrosak = Convert.ToDecimal(reader["UkupanProcenjeniTrosak"]),
                        Status = reader["Status"].ToString(),
                        DatumPodnosenja = Convert.ToDateTime(reader["DatumPodnosenja"])
                    });
                }
            }

            _broker.ZatvoriKonekciju();
            return lista;
        }
    }
}
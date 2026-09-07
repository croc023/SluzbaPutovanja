using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repositories
{
    public class ZaposleniSqlRepo : TehnoloskaKlasa<Zaposleni>
    {
        private const string ConnectionString = "name=SluzbenaPutovanjaDB";

        public override void DodajProcedurom(Zaposleni entitet)
        {
            _broker.OtvoriKonekciju(ConnectionString);
            _broker.PostaviProceduru("sp_DodajZaposlenog");
            _broker.DodajParametar("@ImePrezime", entitet.ImePrezime);
            _broker.IzvršiIzmenu();
            _broker.ZatvoriKonekciju();
        }

        public override void IzmeniProcedurom(Zaposleni entitet)
        {
            _broker.OtvoriKonekciju(ConnectionString);
            _broker.PostaviProceduru("sp_IzmeniZaposlenog");
            _broker.DodajParametar("@ZaposleniID", entitet.ZaposleniID);
            _broker.DodajParametar("@ImePrezime", entitet.ImePrezime);
            _broker.IzvršiIzmenu();
            _broker.ZatvoriKonekciju();
        }

        public override void ObrisiProcedurom(int id)
        {
            _broker.OtvoriKonekciju(ConnectionString);
            _broker.PostaviProceduru("sp_ObrisiZaposlenog");
            _broker.DodajParametar("@ZaposleniID", id);
            _broker.IzvršiIzmenu();
            _broker.ZatvoriKonekciju();
        }

        public override List<Zaposleni> VratiSveProcedurom()
        {
            var lista = new List<Zaposleni>();

            _broker.OtvoriKonekciju(ConnectionString);
            _broker.PostaviProceduru("sp_VratiSveZaposlene");

            using (SqlDataReader reader = _broker.IzvrsiUpit())
            {
                while (reader.Read())
                {
                    lista.Add(new Zaposleni
                    {
                        ZaposleniID = Convert.ToInt32(reader["ZaposleniID"]),
                        ImePrezime = reader["ImePrezime"].ToString()
                    });
                }
            }

            _broker.ZatvoriKonekciju();
            return lista;
        }
    }
}
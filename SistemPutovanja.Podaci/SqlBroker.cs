using System;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka
{
    public class SqlBroker
    {
        private SqlConnection konekcija;
        private SqlCommand komanda;
        private SqlTransaction transakcija;

        public void OtvoriKonekciju(string konekcioniString)
        {
            konekcija = new SqlConnection(konekcioniString);
            komanda = konekcija.CreateCommand();
            konekcija.Open();
        }

        public void PokreniTransakciju() => transakcija = konekcija.BeginTransaction();
        public void PotvrdiTransakciju() => transakcija.Commit();
        public void PonistiTransakciju() => transakcija.Rollback();

        public void ZatvoriKonekciju()
        {
            if (konekcija != null && konekcija.State == ConnectionState.Open)
                konekcija.Close();
        }

        public void PostaviProceduru(string nazivProcedure)
        {
            komanda.Parameters.Clear();
            komanda.CommandText = nazivProcedure;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Transaction = transakcija;
        }

        public void DodajParametar(string naziv, object vrednost)
        {
            komanda.Parameters.AddWithValue(naziv, vrednost ?? DBNull.Value);
        }

        public int IzvršiIzmenu() => komanda.ExecuteNonQuery();
        public SqlDataReader IzvršiCitanje() => komanda.ExecuteReader();
    public SqlDataReader IzvrsiUpit()
        {
            return komanda.ExecuteReader();
        }
    }
}
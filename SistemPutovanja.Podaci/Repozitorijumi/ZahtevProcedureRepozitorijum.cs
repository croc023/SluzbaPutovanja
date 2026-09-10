using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repositories
{
    public class ZahtevProcedureRepozitorijum
    {
        private readonly string _connectionString;

        public ZahtevProcedureRepozitorijum(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ZahtevZaPutovanje> PreuzmiSvePrekoProcedure()
        {
            var lista = new List<ZahtevZaPutovanje>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_PreuzmiSveZahteve", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ZahtevZaPutovanje
                            {
                                BrojZahteva = Convert.ToInt32(reader["BrojZahteva"]),
                                OznakaZahteva = reader["OznakaZahteva"] != DBNull.Value ? reader["OznakaZahteva"].ToString() : string.Empty,
                                ZaposleniID = Convert.ToInt32(reader["ZaposleniID"]),
                                Destinacija = reader["Destinacija"].ToString(),
                                DatumPodnosenja = Convert.ToDateTime(reader["DatumPodnosenja"]),
                                Status = reader["Status"].ToString(),
                                UkupanProcenjeniTrosak = Convert.ToDecimal(reader["UkupanProcenjeniTrosak"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void DodajZahtevPrekoProcedure(ZahtevZaPutovanje zahtev)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DodajZahtev", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ZaposleniID", zahtev.ZaposleniID);
                    cmd.Parameters.AddWithValue("@Destinacija", zahtev.Destinacija ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DatumPodnosenja", zahtev.DatumPodnosenja);
                    cmd.Parameters.AddWithValue("@Status", zahtev.Status ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UkupanProcenjeniTrosak", zahtev.UkupanProcenjeniTrosak);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
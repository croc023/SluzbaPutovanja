using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka.Utils
{
    public class DBUtils
    {
        private static readonly string KonekcioniString =
            ConfigurationManager.ConnectionStrings["AppDbContext"]?.ConnectionString
            ?? @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SluzbenaPutovanjaDB;Integrated Security=True;";

        public static SqlConnection VratiKonekciju()
        {
            return new SqlConnection(KonekcioniString);
        }

        public static DataTable IzvrsiUpit(string sqlUpit, SqlParameter[] parametri = null)
        {
            using (SqlConnection konekcija = VratiKonekciju())
            {
                using (SqlCommand komanda = new SqlCommand(sqlUpit, konekcija))
                {
                    if (parametri != null)
                    {
                        komanda.Parameters.AddRange(parametri);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static int IzvrsiNaredbu(string tekstKomande, CommandType tipKomande = CommandType.Text, SqlParameter[] parametri = null)
        {
            using (SqlConnection konekcija = VratiKonekciju())
            {
                using (SqlCommand komanda = new SqlCommand(tekstKomande, konekcija))
                {
                    komanda.CommandType = tipKomande;
                    if (parametri != null)
                    {
                        komanda.Parameters.AddRange(parametri);
                    }
                    konekcija.Open();
                    return komanda.ExecuteNonQuery();
                }
            }
        }
    }
}
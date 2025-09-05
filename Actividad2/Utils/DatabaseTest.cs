using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Actividad2
{
    public class DatabaseTest
    {
        public static void ProbarConexion()
        {
            string connStr = ConfigurationManager
                .ConnectionStrings["DATABASE"]
                .ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine(@"✅ Conexión exitosa a la base de datos.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"❌ Error al conectar: " + ex.Message);
                }
            }
        }
    }
}
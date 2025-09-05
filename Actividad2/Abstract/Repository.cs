using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Actividad2.Abstract
{
    public abstract class Repository<T>
    {
        private readonly string _connectionString;

        protected Repository()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["DATABASE"]
                .ConnectionString;
        }

        protected abstract string GetTableName();
        protected abstract T Map(IDataRecord record);

        public virtual IEnumerable<T> Listar()
        {
            var result = new List<T>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM {GetTableName()}";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(Map(reader));
                }
            }

            return result;
        }

        public virtual T Buscar(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM {GetTableName()} WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Map(reader);
                }
            }

            return default(T);
        }

        public virtual void Borrar(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = $"DELETE FROM {GetTableName()} WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public virtual void Actualizar(int id, Dictionary<string, object> data)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var setClauses = new List<string>();
                foreach (var col in data.Keys)
                {
                    setClauses.Add($"{col} = @{col}");
                }

                string sql = $"UPDATE {GetTableName()} SET {string.Join(",", setClauses)} WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                foreach (var kvp in data)
                {
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                }

                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
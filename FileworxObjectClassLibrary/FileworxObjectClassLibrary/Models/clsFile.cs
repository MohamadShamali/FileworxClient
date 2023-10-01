using FileworxDTOsLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary.Models
{
    public class clsFile : clsBusinessObject
    {
        // Constants
        static string tableName = "T_FILE";

        // Properties
        public string Body { get; set; }

        public override async Task InsertAsync()
        {
            await base.InsertAsync();

            Body.Replace(Environment.NewLine, "\\n");
            Body = Body.Replace("'", "''");

            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO T_FILE (ID, C_BODY) " +
                               $"VALUES('{Id}', '{Body}');";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();

            Body.Replace(Environment.NewLine, "\\n");
            Body = Body.Replace("'", "''");

            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"UPDATE T_FILE SET C_BODY = '{Body}' " +
                               $"WHERE Id = '{Id}';";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public override async Task ReadAsync()
        {
            await base.ReadAsync();

            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"SELECT ID, C_BODY " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {

                            if (!string.IsNullOrEmpty(reader[1].ToString()))
                            {
                                Body = reader[1].ToString();
                            }
                        }
                    }
                }
            }
        }

        public override void Read()
        {
            base.Read();

            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                connection.Open();
                string query = $"SELECT ID, C_BODY " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            if (!string.IsNullOrEmpty(reader[1].ToString()))
                            {
                                Body = reader[1].ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}

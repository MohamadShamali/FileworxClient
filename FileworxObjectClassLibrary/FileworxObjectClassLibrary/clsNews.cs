using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using FileworxDTOsLibrary;

namespace FileworxObjectClassLibrary
{
    public class clsNews : clsFile
    {
        // Constants
        static string tableName = "T_NEWS";
        private ElasticsearchClientSettings settings;
        private ElasticsearchClient client;

        // Properties
        public string Category { get; set; }

        public clsNews()
        {
            settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            client = new ElasticsearchClient(settings);

            Class = Type.News;
        }

        public async override Task InsertAsync()
        {
            await base.InsertAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO T_NEWS (ID, C_CATEGORY) " +
                               $"VALUES('{Id}', '{Category}')";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            var response = await client.IndexAsync(this, EditBeforeRun.ElasticFilesIndex);

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforeRun.ElasticFilesIndex); // refresh index 
        }

        public async override Task DeleteAsync()
        {
            await base.DeleteAsync();
            var response = await client.DeleteAsync(EditBeforeRun.ElasticFilesIndex, Id);

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforeRun.ElasticFilesIndex);
        }

        public async override Task UpdateAsync()
        {
            await base.UpdateAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"UPDATE T_NEWS SET C_CATEGORY = '{Category}' " +
                               $"WHERE Id = '{Id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            var response = await client.UpdateAsync<clsNews, clsNews>(EditBeforeRun.ElasticFilesIndex, Id, u => u.Doc(this));

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforeRun.ElasticFilesIndex);
        }

        public override async Task ReadAsync()
        {
            await base.ReadAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"SELECT ID, C_CATEGORY " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            Category = (reader[1].ToString());
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
                string query = $"SELECT ID, C_CATEGORY " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category = (reader[1].ToString());
                        }
                    }
                }
            }
        }
    }
}

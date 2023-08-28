using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;

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
            settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
            client = new ElasticsearchClient(settings);

            Class = Type.News;
        }

        public async override void Insert()
        {
            base.Insert();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO T_NEWS (ID, C_CATEGORY) " +
                               $"VALUES('{Id}', '{Category}')";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            var response = await client.IndexAsync(this, "businessobject");

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
        }

        public async override void Delete()
        {
            base.Delete();
            var response = await client.DeleteAsync("businessobject", Id);

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
        }

        public async override void Update()
        {
            base.Update();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                connection.Open();

                string query = $"UPDATE T_NEWS SET C_CATEGORY = '{Category}' " +
                               $"WHERE Id = '{Id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            var response = await client.UpdateAsync<clsNews, clsNews>("businessobject", Id, u => u.Doc(this));

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
        }

        public override void Read()
        {
            base.Read();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
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
